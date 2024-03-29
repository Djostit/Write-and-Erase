﻿namespace Pishi_Wash__Store.ViewModels
{
    public class BrowseProductViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly ProductService _productService;
        public List<string> Sorts { get; set; } = new() { "По умолчанию", "По возрастанию", "По убыванию" };
        public List<string> Filters { get; set; } = new() { "Все диапазоны", "0-5%", "5-9%", "9% и более" };
        public bool IsEnabledCart { get; set; }
        public List<DbProduct> Products { get; set; }
        public DbProduct SelectedProduct { get; set; }
        public string FullName { get; set; } = UserSetting.Default.UserName == string.Empty ? "Гость" : $"{UserSetting.Default.UserSurname} {UserSetting.Default.UserName} {UserSetting.Default.UserPatronymic}";
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

        public BrowseProductViewModel(PageService pageService, ProductService productService)
        {
            _pageService = pageService;
            _productService = productService;
            CheckEnabled();
        }

        private void CheckEnabled() => IsEnabledCart = Global.CurrentCart.Any(c => c.ArticleName != null);

        private async void UpdateProduct()
        {
            var currentProduct = await _productService.GetProducts();
            MaxRecords = currentProduct.Count;

            if (!string.IsNullOrEmpty(SelectedFilter))
            {
                switch (SelectedFilter)
                {
                    case "0-5%":
                        currentProduct = currentProduct.Where(p => (float)p.ProductDiscountAmount is >= 0 and < 5).ToList();
                        break;
                    case "5-9%":
                        currentProduct = currentProduct.Where(p => (float)p.ProductDiscountAmount is >= 5 and < 9).ToList();
                        break;
                    case "9% и более":
                        currentProduct = currentProduct.Where(p => (float)p.ProductDiscountAmount >= 9).ToList();
                        break;
                }
            }

            if (!string.IsNullOrEmpty(Search))
                currentProduct = currentProduct.Where(p => p.ProductNameNavigation.ProductName.ToLower().Contains(Search.ToLower())).ToList();

            if (!string.IsNullOrEmpty(SelectedSort))
            {
                switch (SelectedSort)
                {
                    case "По возрастанию":
                        currentProduct = currentProduct.OrderBy(p => p.ProductCost).ToList();
                        break;
                    case "По убыванию":
                        currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
                        break;
                }
            }

            Records = currentProduct.Count;
            Products = currentProduct;
        }

        public DelegateCommand SignOutCommand => new(() =>
        {

            UserSetting.Default.UserName = string.Empty;
            UserSetting.Default.UserSurname = string.Empty;
            UserSetting.Default.UserPatronymic = string.Empty;
            UserSetting.Default.UserRole = string.Empty;
            Global.CurrentCart.Clear();
            _pageService.ChangePage(new SingInPage());
        });
        public DelegateCommand AddToCartCommand => new(() =>
        {
            var cart = Global.CurrentCart.SingleOrDefault(c => c.ArticleName.Equals(SelectedProduct.ProductArticleNumber));
            if (cart == null)
            {
                Global.CurrentCart.Add(new Cart
                {
                    ArticleName = SelectedProduct.ProductArticleNumber,
                    Count = 1
                });
            }
            else
            {
                cart.Count++;
                Global.CurrentCart[Global.CurrentCart.FindIndex(c => c.ArticleName.Equals(cart.ArticleName))] = cart;
            }
            CheckEnabled();
        });
        public DelegateCommand CartCommand => new(() =>
        {
            _pageService.ChangePage(new CartPage());
        });
    }
}
