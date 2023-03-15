﻿namespace Pishi_Wash__Store.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;
        private readonly DocumentService _documentService;
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorMessageButton { get; set; }
        public SignInViewModel(UserService userService, PageService pageService, DocumentService documentService)
        {
            _userService = userService;
            _pageService = pageService;
            _documentService = documentService;
        }
        public AsyncCommand SignInCommand => new(async () =>
        {
            await Task.Run(async () =>
            {
                if (await _userService.AuthorizationAsync(Username, Password))
                {
                    ErrorMessageButton = string.Empty;
                    await Application.Current.Dispatcher.InvokeAsync(async () => 
                    {
                        if (UserSetting.Default.UserRole == "Клиент")
                            _pageService.ChangePage(new BrowseProductPage());
                        else
                            _pageService.ChangePage(new BrowseAdminPage());
                    });
                }
                else
                    ErrorMessageButton = "Неверный логин или пароль";
            });
        }, bool () =>
        {
            if (string.IsNullOrWhiteSpace(Username)
                || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Пустые поля";
                ErrorMessageButton = string.Empty;
            }
            else
                ErrorMessage = string.Empty;

            if (ErrorMessage.Equals(string.Empty))
                return true; return false;
        });
        public DelegateCommand SignUpCommand => new(async() => _pageService.ChangePage(new SignUpPage()));
        public DelegateCommand SignInLaterCommand => new(() => 
        {
#if DEBUG
            _pageService.ChangePage(new BrowseAdminPage());
#else
            _pageService.ChangePage(new BrowseProductPage()); 
#endif
        });
    }
}
