﻿using Microsoft.Win32;
using Pishi_Wash__Store.Data.Models;

namespace Pishi_Wash__Store.ViewModels
{
    public class HelpAdminViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly ProductService _productService;
        public List<string> Sorts { get; set; } = new() { "По возрастанию", "По убыванию" };
        public List<string> Filters { get; set; } = new() { "Все диапазоны", "Бумага офисная", "Для офиса", "Тетради школьные", "Школьные пренадлежности", "Школьные принадлежности" };
        public ObservableCollection<DbProduct> Products { get; set; }
        public ObservableCollection<Pmanufacturer> Pmanufacturers { get; set; }
        public ObservableCollection<Pcategory> Pcategories { get; set; }
        public ObservableCollection <Pprovider> Pproviders { get; set; }
        public ObservableCollection <Pname> Pnames { get; set; }
        public string FullName { get; set; } = UserSetting.Default.UserName == string.Empty ? "Гость" : $"{UserSetting.Default.UserSurname} {UserSetting.Default.UserName} {UserSetting.Default.UserPatronymic}";
        public HelpAdminViewModel(PageService pageService, ProductService productService)
        {
            _pageService = pageService;
            _productService = productService;
        }
        public int? MaxRecords { get; set; } = 0;
        public int? Records { get; set; } = 0;
        public string SelectedSort
        {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: UpdateProduct); }
        }
        public string SelectedFilter
        {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: UpdateProduct); }
        }
        public string Search
        {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: UpdateProduct); }
        }

        public TabItem SelectedItemTab
        {
            get { return GetValue<TabItem>(); }
            set { SetValue(value, changedCallback: UpdateProduct); }
        }
        private void SwitchItemTab()
        {
            if (SelectedItemTab == null)
                return;
            switch (SelectedItemTab.Header)
            {
                case "Продукты":
                    Filters = new() { "Все диапазоны", "Бумага офисная", "Для офиса", "Тетради школьные", "Школьные пренадлежности", "Школьные принадлежности" };
                    break;
                case "Поставщики":
                    Filters = new() { "Все диапазоны" };
                    break;
                case "Категории":
                    Filters = new() { "Все диапазоны" };
                    break;
            }
        }
        private async void UpdateProduct()
        {
            if (SelectedItemTab == null)
                return;
            SwitchItemTab();
            switch (SelectedItemTab.Header)
            {
                case "Продукты":
                    await Task.Run(async () =>
                    {
                        if (SelectedFilter != "Все диапазоны")
                            _productService.GetPcategories();
                        var currentProducts = await _productService.GetProducts();
                        MaxRecords = currentProducts.Count;

                        if (!string.IsNullOrEmpty(SelectedFilter))
                        {
                            switch (SelectedFilter)
                            {
                                case "Бумага офисная":
                                    currentProducts = currentProducts.Where(c => c.ProductCategoryNavigation.ProductCategory == "Бумага офисная").ToList();
                                    break;
                                case "Для офиса":
                                    currentProducts = currentProducts.Where(c => c.ProductCategoryNavigation.ProductCategory == "Для офиса").ToList();
                                    break;
                                case "Тетради школьные":
                                    currentProducts = currentProducts.Where(c => c.ProductCategoryNavigation.ProductCategory == "Тетради школьные").ToList();
                                    break;
                                case "Школьные пренадлежности":
                                    currentProducts = currentProducts.Where(c => c.ProductCategoryNavigation.ProductCategory == "Школьные пренадлежности").ToList();
                                    break;
                                case "Школьные принадлежности":
                                    currentProducts = currentProducts.Where(c => c.ProductCategoryNavigation.ProductCategory == "Школьные принадлежности").ToList();
                                    break;
                            }
                        }

                        if (!string.IsNullOrEmpty(Search))
                            currentProducts = currentProducts.Where(p => p.ProductNameNavigation.ProductName
                            .ToString()
                            .ToLower()
                            .Contains(Search.ToLower())
                            || p.ProductArticleNumber.ToLower().Contains(Search.ToLower())).ToList();

                        if (!string.IsNullOrEmpty(SelectedSort))
                        {
                            switch (SelectedSort)
                            {
                                case "По возрастанию":
                                    currentProducts = currentProducts.OrderBy(o => o.ProductNameNavigation.ProductName).ToList();
                                    break;
                                case "По убыванию":
                                    currentProducts = currentProducts.OrderByDescending(o => o.ProductNameNavigation.ProductName).ToList();
                                    break;
                            }
                        }

                        Records = currentProducts.Count;
                        Products = new ObservableCollection<DbProduct>(currentProducts);

                    });
                    break;

                case "Производители":
                    await Task.Run(() =>
                    {
                        var currnetPmanufacturers = _productService.GetPmanufacturers();
                        MaxRecords = currnetPmanufacturers.Count;

                        if (!string.IsNullOrEmpty(Search))
                            currnetPmanufacturers = currnetPmanufacturers.Where(p => p.ProductManufacturer
                            .ToString()
                            .ToLower()
                            .Contains(Search.ToLower())).ToList();

                        if (!string.IsNullOrEmpty(SelectedSort))
                        {
                            switch (SelectedSort)
                            {
                                case "По возрастанию":
                                    currnetPmanufacturers = currnetPmanufacturers.OrderBy(o => o.PmanufacturerId).ToList();
                                    break;
                                case "По убыванию":
                                    currnetPmanufacturers = currnetPmanufacturers.OrderByDescending(o => o.PmanufacturerId).ToList();
                                    break;
                            }
                        }

                        Records = currnetPmanufacturers.Count;
                        Pmanufacturers = new ObservableCollection<Pmanufacturer>(currnetPmanufacturers);
                    });
                    break;

                case "Категории":
                    await Task.Run(() =>
                    {
                        var currentPcategories = _productService.GetPcategories();
                        MaxRecords = currentPcategories.Count;

                        if (!string.IsNullOrEmpty(Search))
                            currentPcategories = currentPcategories.Where(p => p.ProductCategory
                            .ToString()
                            .ToLower()
                            .Contains(Search.ToLower())).ToList();

                        if (!string.IsNullOrEmpty(SelectedSort))
                        {
                            switch (SelectedSort)
                            {
                                case "По возрастанию":
                                    currentPcategories = currentPcategories.OrderBy(o => o.PcategoryId).ToList();
                                    break;
                                case "По убыванию":
                                    currentPcategories = currentPcategories.OrderByDescending(o => o.PcategoryId).ToList();
                                    break;
                            }
                        }

                        Records = currentPcategories.Count;
                        Pcategories = new ObservableCollection<Pcategory>(currentPcategories);
                    });
                    break;
                case "Поставщики":
                    await Task.Run(() =>
                    {
                        var currentProvider = _productService.GetProdivers();
                        MaxRecords = currentProvider.Count;

                        if (!string.IsNullOrEmpty(Search))
                            currentProvider = currentProvider.Where(p => p.ProductProvider
                            .ToString()
                            .ToLower()
                            .Contains(Search.ToLower())).ToList();

                        if (!string.IsNullOrEmpty(SelectedSort))
                        {
                            switch (SelectedSort)
                            {
                                case "По возрастанию":
                                    currentProvider = currentProvider.OrderBy(o => o.PproviderId).ToList();
                                    break;
                                case "По убыванию":
                                    currentProvider = currentProvider.OrderByDescending(o => o.PproviderId).ToList();
                                    break;
                            }
                        }

                        Records = currentProvider.Count;
                        Pproviders = new ObservableCollection<Pprovider>(currentProvider);
                    });
                    break;
                case "Имена":
                    await Task.Run(() =>
                    {
                        var currentName = _productService.GetNames();
                        MaxRecords = currentName.Count;

                        if (!string.IsNullOrEmpty(Search))
                            currentName = currentName.Where(p => p.ProductName
                            .ToString()
                            .ToLower()
                            .Contains(Search.ToLower())).ToList();

                        if (!string.IsNullOrEmpty(SelectedSort))
                        {
                            switch (SelectedSort)
                            {
                                case "По возрастанию":
                                    currentName = currentName.OrderBy(o => o.PnameId).ToList();
                                    break;
                                case "По убыванию":
                                    currentName = currentName.OrderByDescending(o => o.PnameId).ToList();
                                    break;
                            }
                        }

                        Records = currentName.Count;
                        Pnames = new ObservableCollection<Pname>(currentName);
                    });
                    break;

            }
        }

        #region Product
        public DbProduct SelectedProduct { get; set; }

        // Редактирование
        public bool IsDialogEditProductOpen { get; set; } = false;
        public string EditProduct { get; set; }
        public bool IsProductStatus { get; set; }
        public string EditDiscount { get; set; }

        public DelegateCommand EditProductCommand => new(() =>
        {
            if (SelectedProduct == null)
                return;

            IsProductStatus = SelectedProduct.ProductStatus == "";
            EditDiscount = SelectedProduct.ProductDiscountAmount.ToString();
            IsDialogEditProductOpen = true;
        });

        public DelegateCommand SaveCurrentProductCommand => new(async () =>
        {
            if (SelectedProduct.ProductDiscountAmount.ToString() != EditDiscount
            || SelectedProduct.ProductStatus == "" != IsProductStatus)
            {
                var item = Products.First(i => i.ProductArticleNumber == SelectedProduct.ProductArticleNumber);
                var index = Products.IndexOf(item);
                item.ProductDiscountAmount = sbyte.Parse(EditDiscount);
                if (SelectedProduct.ProductStatus == "" != IsProductStatus)
                {
                    item.ProductStatus = IsProductStatus ? "" : "Не активен";
                }


                Products.RemoveAt(index);
                Products.Insert(index, item);
                await _productService.UpdateCurrentProduct(item);
            }
            IsDialogEditProductOpen = false;
        });

        // Добавление 
        public bool IsDialogAddProductOpen { get; set; } = false;
        public string ProductArticle { get; set; }
        public Pname ProductSelectedName { get; set; }
        public string ProductDescription { get; set; }
        public Pcategory ProductSelectedCategories { get; set; }
        public string ProductImage { get; set; }
        public Pmanufacturer ProductSelectedManufacturer { get; set; }
        public Pprovider ProductSelectedProvider { get; set; }
        public string ProductPrice { get; set; }
        public string ProductDiscount { get; set;}
        public string ProductCountInStock { get; set; }

        public DelegateCommand AddProductCommand => new(() =>
        {
            IsDialogAddProductOpen = true;
        });

        public DelegateCommand SaveAddProductCommand => new(async () =>
        {
            //if (!string.IsNullOrWhiteSpace(AddManufacturers)
            //&& !Pmanufacturers.Any(p => p.ProductManufacturer == AddManufacturers))
            //{
            //    Pmanufacturers.Insert(0, await _productService.AddManufacturersAsync(new Pmanufacturer
            //    {
            //        ProductManufacturer = AddManufacturers
            //    }));
            //}
            if (!Products.Any(p => p.ProductArticleNumber == ProductArticle))
            {
                Products.Insert(0, await _productService.AddProductAsync(new Product
                {
                    ProductArticleNumber = ProductArticle,
                    ProductCost = float.Parse(ProductPrice),
                    ProductDescription = ProductDescription,
                    ProductPhoto = ProductImage == null ? "" : ProductImage,
                    ProductName = ProductSelectedName.PnameId,
                    ProductCategory = ProductSelectedCategories.PcategoryId,
                    ProductManufacturer = ProductSelectedManufacturer.PmanufacturerId,
                    ProductProvider = ProductSelectedProvider.PproviderId,
                    ProductDiscountAmount = sbyte.Parse(ProductDiscount),
                    ProductQuantityInStock = int.Parse(ProductCountInStock),
                    ProductStatus = "",
                }));
            }
            IsDialogAddProductOpen = false;
        }, bool() =>
        {
            return !string.IsNullOrWhiteSpace(ProductArticle)
            && ProductSelectedName != null
            && !string.IsNullOrWhiteSpace(ProductDescription)
            && ProductSelectedCategories != null
            && ProductSelectedManufacturer != null
            && ProductSelectedProvider != null
            && !string.IsNullOrWhiteSpace(ProductPrice)
            && !string.IsNullOrWhiteSpace(ProductDiscount)
            && !string.IsNullOrWhiteSpace(ProductCountInStock);
        });

        public DelegateCommand ChoiceImageCommand => new(() => 
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Выберите изображение",
                Filter = "Изображения (*.jpg, *jpeg, *.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ProductImage  = Path.GetFileName(openFileDialog.FileName);
            }
        });

        #endregion

        #region Caterories

        public Pcategory SelectedCategories { get; set; }

        // Редактирование
        public bool IsDialogEditCategoriesOpen { get; set; } = false;
        public string EditCategories { get; set; }

        public DelegateCommand EditCategoriesCommand => new(() =>
        {
            if (SelectedCategories == null)
                return;
            EditCategories = SelectedCategories.ProductCategory;
            IsDialogEditCategoriesOpen = true;
        });

        public DelegateCommand SaveCurrentCategoriesCommand => new(async () =>
        {
            if (SelectedCategories.ProductCategory != EditCategories
            && !Pcategories.Any(p => p.ProductCategory == EditCategories))
            {
                var item = Pcategories.First(i => i.PcategoryId == SelectedCategories.PcategoryId);
                var index = Pcategories.IndexOf(item);
                item.ProductCategory = EditCategories;

                Pcategories.RemoveAt(index);
                Pcategories.Insert(index, item);
                await _productService.SaveChangesAsync();
            }
            IsDialogEditCategoriesOpen = false;
        });

        // Добавление 
        public bool IsDialogAddCategoriesOpen { get; set; } = false;
        public string AddCategories { get; set; } 
        public DelegateCommand AddCategoriesCommand => new(() =>
        {
            AddCategories = string.Empty;
            IsDialogAddCategoriesOpen = true;
        });

        public DelegateCommand SaveAddCategoriesCommand => new(async () =>
        {
            if (!string.IsNullOrWhiteSpace(AddCategories)
            && !Pcategories.Any(p => p.ProductCategory == AddCategories))
            {
                Pcategories.Insert(0, await _productService.AddCategoriesAsync(new Pcategory 
                { 
                    ProductCategory = AddCategories 
                }));
            }
            IsDialogAddCategoriesOpen = false;
        }, bool () =>
        {
            return !string.IsNullOrWhiteSpace(AddCategories);
        });

        #endregion

        #region Manufacturers

        public Pmanufacturer SelectedManufacturers { get; set; }

        // Редактирование
        public bool IsDialogEditManufacturersOpen { get; set; } = false;
        public string EditManufacturers { get; set; }

        public DelegateCommand EditManufacturersCommand => new(() =>
        {
            if (SelectedManufacturers == null)
                return;
            EditManufacturers = SelectedManufacturers.ProductManufacturer;
            IsDialogEditManufacturersOpen = true;
        });

        public DelegateCommand SaveCurrentManufacturersCommand => new(async () =>
        {
            if (SelectedManufacturers.ProductManufacturer != EditManufacturers
            && !Pmanufacturers.Any(p => p.ProductManufacturer == EditManufacturers))
            {
                var item = Pmanufacturers.First(i => i.PmanufacturerId == SelectedManufacturers.PmanufacturerId);
                var index = Pmanufacturers.IndexOf(item);
                item.ProductManufacturer = EditManufacturers;

                Pmanufacturers.RemoveAt(index);
                Pmanufacturers.Insert(index, item);
                await _productService.SaveChangesAsync();
            }
            IsDialogEditManufacturersOpen = false;
        });

        // Добавление 
        public bool IsDialogAddManufacturersOpen { get; set; } = false;
        public string AddManufacturers { get; set; }
        public DelegateCommand AddManufacturersCommand => new(() =>
        {
            AddManufacturers = string.Empty;
            IsDialogAddManufacturersOpen = true;
        });

        public DelegateCommand SaveAddManufacturersCommand => new(async () =>
        {
            if (!string.IsNullOrWhiteSpace(AddManufacturers)
            && !Pmanufacturers.Any(p => p.ProductManufacturer == AddManufacturers))
            {
                Pmanufacturers.Insert(0, await _productService.AddManufacturersAsync(new Pmanufacturer
                {
                    ProductManufacturer = AddManufacturers
                }));
            }
            IsDialogAddManufacturersOpen = false;
        }, bool () =>
        {
            return !string.IsNullOrWhiteSpace(AddManufacturers);
        });


        #endregion

        #region Provider

        public Pprovider SelectedProvider { get; set; }

        // Редактирование
        public bool IsDialogEditProviderOpen { get; set; } = false;
        public string EditProvider { get; set; }

        public DelegateCommand EditProviderCommand => new(() =>
        {
            if (SelectedProvider == null)
                return;
            EditProvider = SelectedProvider.ProductProvider;
            IsDialogEditProviderOpen = true;
        });

        public DelegateCommand SaveCurrentProviderCommand => new(async () =>
        {
            if (SelectedProvider.ProductProvider != EditProvider
            && !Pproviders.Any(p => p.ProductProvider == EditProvider))
            {
                var item = Pproviders.First(i => i.PproviderId == SelectedProvider.PproviderId);
                var index = Pproviders.IndexOf(item);
                item.ProductProvider = EditProvider;

                Pproviders.RemoveAt(index);
                Pproviders.Insert(index, item);
                await _productService.SaveChangesAsync();
            }
            IsDialogEditProviderOpen = false;
        });

        // Добавление 
        public bool IsDialogAddProviderOpen { get; set; } = false;
        public string AddProvider { get; set; }
        public DelegateCommand AddProviderCommand => new(() =>
        {
            AddProvider = string.Empty;
            IsDialogAddProviderOpen = true;
        });

        public DelegateCommand SaveAddProviderCommand => new(async () =>
        {
            if (!string.IsNullOrWhiteSpace(AddProvider)
            && !Pproviders.Any(p => p.ProductProvider == AddProvider))
            {
                Pproviders.Insert(0, await _productService.AddProviderAsync(new Pprovider
                {
                    ProductProvider = AddProvider,
                }));
            }
            IsDialogAddProviderOpen = false;
        }, bool () =>
        {
            return !string.IsNullOrWhiteSpace(AddProvider);
        });

        #endregion

        #region Name

        public Pname SelectedName { get; set; }

        // Редактирование
        public bool IsDialogEditNameOpen { get; set; } = false;
        public string EditName { get; set; }

        public DelegateCommand EditNameCommand => new(() =>
        {
            if (SelectedName == null)
                return;
            EditName = SelectedName.ProductName;
            IsDialogEditNameOpen = true;
        });

        public DelegateCommand SaveCurrentNameCommand => new(async () =>
        {
            if (SelectedName.ProductName != EditName
            && !Pnames.Any(p => p.ProductName == EditName))
            {
                var item = Pnames.First(i => i.PnameId == SelectedName.PnameId);
                var index = Pnames.IndexOf(item);
                item.ProductName = EditName;

                Pnames.RemoveAt(index);
                Pnames.Insert(index, item);
                await _productService.SaveChangesAsync();
            }
            IsDialogEditNameOpen = false;
        });

        // Добавление 
        public bool IsDialogAddNameOpen { get; set; } = false;
        public string AddName { get; set; }
        public DelegateCommand AddNameCommand => new(() =>
        {
            AddName = string.Empty;
            IsDialogAddNameOpen = true;
        });

        public DelegateCommand SaveAddNameCommand => new(async () =>
        {
            if (!string.IsNullOrWhiteSpace(AddName)
            && !Pnames.Any(p => p.ProductName == AddName))
            {
                Pnames.Insert(0, await _productService.AddNameAsync(new Pname
                {
                    ProductName = AddName,
                }));
            }
            IsDialogAddNameOpen = false;
        }, bool () =>
        {
            return !string.IsNullOrWhiteSpace(AddName);
        });

        #endregion
        public DelegateCommand BrowseAdminCommand => new(() => _pageService.ChangePage(new BrowseAdminPage()));
        public DelegateCommand SignOutCommand => new(() =>
        {

            UserSetting.Default.UserName = string.Empty;
            UserSetting.Default.UserSurname = string.Empty;
            UserSetting.Default.UserPatronymic = string.Empty;
            UserSetting.Default.UserRole = string.Empty;
            _pageService.ChangePage(new SingInPage());
        });
    }
}
