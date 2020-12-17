using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using TLSharp.Core;

namespace TelegramClient
{
    /// <summary>
    /// The top-level window to show
    /// </summary>
    internal class TelegramTerminalClientWindow : Window
    {
        #region Fields Members
        private bool _loggedIn;
        //private View _activeView;
        private ColorScheme _colorScheme;
        private TLSharp.Core.TelegramClient _client;
        #endregion Fields Members

        #region Properties
        #endregion Properties

        #region Constructors
        public TelegramTerminalClientWindow()
        {
            //this.Loaded += TelegramTerminalClientWindow_LoadedAsync;
            //this.Ready += TelegramTerminalClientWindow_Ready;
            //this.Added += TelegramTerminalClientWindow_Added;
            //this.DrawContent += TelegramTerminalClientWindow_DrawContent;
            //this.Enter += TelegramTerminalClientWindow_Enter;
            //this.Initialized += TelegramTerminalClientWindow_Initialized;
            //this.LayoutComplete += TelegramTerminalClientWindow_LayoutComplete;
            //this.LayoutStarted += TelegramTerminalClientWindow_LayoutStarted;
            //this.Leave += TelegramTerminalClientWindow_Leave;
            //this.Removed += TelegramTerminalClientWindow_Removed;
            //this.Unloaded += TelegramTerminalClientWindow_Unloaded;

            var top = Application.Top;

            _colorScheme = new ColorScheme();

            this.Title = "Telegram Client";
            this.X = 0;
            this.Y = 1; // Leave one row for the toplevel menu

            // By using Dim.Fill(), it will automatically resize without manual intervention
            this.Width = Dim.Fill();
            this.Height = Dim.Fill(1);

            this.Add(new FrameView("TEST1")
            {
                Width = 25,
                Height = 25,
                X = 0,
                Y = 1 // for menu
            });

            var apiId = ;
            var apiHash = "";

            _client = new TLSharp.Core.TelegramClient(apiId, apiHash);

            SetupLayoutAsync(3);

            top.Add(this);
        }

        private void TelegramTerminalClientWindow_Unloaded()
        {
            throw new NotImplementedException();
        }

        private void TelegramTerminalClientWindow_Removed(View obj)
        {
            throw new NotImplementedException();
        }

        private void TelegramTerminalClientWindow_Leave(FocusEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void TelegramTerminalClientWindow_LayoutStarted(LayoutEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void TelegramTerminalClientWindow_LayoutComplete(LayoutEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void TelegramTerminalClientWindow_Initialized(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TelegramTerminalClientWindow_Enter(FocusEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void TelegramTerminalClientWindow_DrawContent(Rect obj)
        {
            throw new NotImplementedException();
        }

        private async void TelegramTerminalClientWindow_Added(View obj)
        {
            await SetupLayoutAsync(2);
            throw new NotImplementedException();
        }

        private async Task SetupLayoutAsync(int x)
        {
            await _client.ConnectAsync();
            if (_client.IsUserAuthorized())
            {
                //_activeView = new FrameView();
                //MainView(_activeView);
                MainView(this);
            }
            else
            {
                //var _activeView = new Dialog("Login");
                //LoginView(_activeView);
                var _activeView = LoginView(new Dialog("Login"));
                this.Add(_activeView);
            }

            //this.Add(_activeView);
            //throw new NotImplementedException();
        }

        private void TelegramTerminalClientWindow_Ready()
        {
            int x = 5;
            throw new NotImplementedException();
        }

        private async void TelegramTerminalClientWindow_LoadedAsync()
        {
            await SetupLayoutAsync(1);
        }
        #endregion Constructors

        #region Events/Actions
        private void VerifyNumberView(string phoneNumber, View view)
        {
            //TODO: Add a number verification
            var phoneNumberLabel = new Label($"Your phone number is {phoneNumber}?")
            {
                X = Pos.Center(),
                Y = Pos.Percent(40)
            };

            var btnApprove = new Button("Approve")
            {
                X = Pos.Center() - 15,
                Y = Pos.Percent(50),
                IsDefault = true
            };
            btnApprove.Clicked += () => LoginCodeView(view, phoneNumber);

            var btnFix = new Button("Fix")
            {
                X = Pos.Center() + 1,
                Y = Pos.Percent(50)
            };
            btnFix.Clicked += () => LoginView(view);

            view.RemoveAll();
            view.Add(phoneNumberLabel, btnApprove, btnFix);
        }

        private void LoginCodeView(View view, string phoneNumber)
        {
            var hash = GetLoginCode(phoneNumber);

            var loginCodeLabel = new Label("Password: ")
            {
                X = Pos.Center(),
                Y = Pos.Percent(50)
            };

            var loginCodeText = new TextField("")
            {
                Secret = false,
                X = Pos.Right(loginCodeLabel) + 1,
                Y = Pos.Top(loginCodeLabel),
                Width = 7
            };

            var btnLogin = new Button("Log in")
            {
                X = Pos.Center(),
                Y = Pos.Top(loginCodeLabel) + 1
            };
            btnLogin.Clicked += () => Login(phoneNumber, loginCodeText.Text.ToString(), hash.Result);

            view.RemoveAll();
            view.Add(loginCodeLabel, loginCodeText, btnLogin);
        }

        private void KeyDownHandler(KeyEventEventArgs obj)
        {
            Console.WriteLine("NOT IMPLEMENTED: KeyDownHandler");
            //throw new NotImplementedException();
        }

        private void _scenarioListView_OpenSelectedItem(ListViewItemEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private void CategoryListView_SelectedChanged(ListViewItemEventArgs obj)
        {
            Console.WriteLine("NOT IMPLEMENTED: CategoryListView_SelectedChanged");
            //throw new NotImplementedException();
        }

        private bool Quit()
        {
            Application.RequestStop();
            return true;
        }

        private void Close()
        {
            throw new System.NotImplementedException();
        }

        private void NewFile()
        {
            throw new System.NotImplementedException();
        }
        #endregion Events/Actions

        #region Help Methods
        private void MainView(View view)
        {
            //view = Application.Top;
            //Application.UseSystemConsole = _useSystemConsole;
            //Application.Init();

            // Set this here because not initialized until driver is loaded
            //_baseColorScheme = Colors.Base;

            StringBuilder aboutMessage = new StringBuilder();
            aboutMessage.AppendLine("Cross Platform Telegram Console UI using .NET and GUI.cs");
            aboutMessage.AppendLine(@"             _           ");
            aboutMessage.AppendLine(@"  __ _ _   _(_)  ___ ___ ");
            aboutMessage.AppendLine(@" / _` | | | | | / __/ __|");
            aboutMessage.AppendLine(@"| (_| | |_| | || (__\__ \");
            aboutMessage.AppendLine(@" \__, |\__,_|_(_)___|___/");
            aboutMessage.AppendLine(@" |___/                   ");
            aboutMessage.AppendLine("");
            aboutMessage.AppendLine($"Version: {typeof(TelegramTerminalClientWindow).Assembly.GetName().Version}");
            aboutMessage.AppendLine($"Using Terminal.Gui Version: {typeof(Terminal.Gui.Application).Assembly.GetName().Version}");
            aboutMessage.AppendLine("");

            // Creates a menubar, the item "New" has a help menu.
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File2", new[] {
                    new MenuItem ("_New", "Creates new file", NewFile),
                    new MenuItem ("_Close", "", Close),
                    new MenuItem ("_Quit", "", () => { if (Quit ()) this.Running = false; }, null, null, Key.Q | Key.CtrlMask) //top.Running => this.Running
                }),
                new MenuBarItem ("_Edit", new[] {
                    new MenuItem ("_Copy", "", null),
                    new MenuItem ("C_ut", "", null),
                    new MenuItem ("_Paste", "", null)
                }),
                //new MenuBarItem ("_Color Scheme", CreateColorSchemeMenuItems()),
                //new MenuBarItem ("Diag_nostics", CreateDiagnosticMenuItems()),
                new MenuBarItem ("_Help", new MenuItem [] {
                    //new MenuItem ("_gui.cs API Overview", "", () => OpenUrl ("https://migueldeicaza.github.io/gui.cs/articles/overview.html"), null, null, Key.F1),
                    //new MenuItem ("gui.cs _README", "", () => OpenUrl ("https://github.com/migueldeicaza/gui.cs"), null, null, Key.F2),
                    new MenuItem ("_About...", "About this app", () =>  MessageBox.Query ("About UI Catalog", aboutMessage.ToString(), "_Ok"), null, null, Key.CtrlMask | Key.A),
                }),
            });

            var _leftPane = new FrameView("Contacts")
            {
                X = 0,
                Y = 0, // for menu
                Width = 25,
                Height = Dim.Fill(),
                CanFocus = false,
                Shortcut = Key.CtrlMask | Key.C
            };
            _leftPane.Title = $"{_leftPane.Title} ({_leftPane.ShortcutTag})";
            _leftPane.ShortcutAction = () => _leftPane.SetFocus();

            var _topRightPane = new FrameView("Chat Window")
            {
                X = Pos.Right(_leftPane),
                Y = Pos.Top(_leftPane), // for menu
                Width = Dim.Fill(),
                Height = Dim.Fill(10),
                CanFocus = true,
                Shortcut = Key.CtrlMask | Key.S
            };
            _topRightPane.Title = $"{_topRightPane.Title} ({_topRightPane.ShortcutTag})";
            _topRightPane.ShortcutAction = () => _topRightPane.SetFocus();

            var _bottomRightPane = new FrameView("Input")
            {
                X = Pos.Left(_topRightPane),
                Y = Pos.Bottom(_topRightPane), // for menu
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                CanFocus = true,
                Shortcut = Key.CtrlMask | Key.S
            };
            _bottomRightPane.Title = $"{_bottomRightPane.Title} ({_bottomRightPane.ShortcutTag})";
            _bottomRightPane.ShortcutAction = () => _bottomRightPane.SetFocus();

            var _categories = new List<string>() { };
            //_categories = Scenario.GetAllCategories().OrderBy(c => c).ToList();
            var _categoryListView = new ListView(_categories)
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(0),
                Height = Dim.Fill(0),
                AllowsMarking = false,
                CanFocus = true,
            };
            _categoryListView.OpenSelectedItem += (a) =>
            {
                _topRightPane.SetFocus();
            };
            _categoryListView.SelectedItemChanged += CategoryListView_SelectedChanged;
            _leftPane.Add(_categoryListView);

            //_nameColumnWidth = Scenario.ScenarioMetadata.GetName(_scenarios.OrderByDescending(t => Scenario.ScenarioMetadata.GetName(t).Length).FirstOrDefault()).Length;

            var _scenarioListView = new ListView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(0),
                Height = Dim.Fill(0),
                AllowsMarking = false,
                CanFocus = true,
            };

            _scenarioListView.OpenSelectedItem += _scenarioListView_OpenSelectedItem;
            _topRightPane.Add(_scenarioListView);

            var _categoryListViewItem = -1;
            _categoryListView.SelectedItem = _categoryListViewItem;
            _categoryListView.OnSelectedChanged();

            var _capslock = new StatusItem(Key.CharMask, "Caps", null);
            var _numlock = new StatusItem(Key.CharMask, "Num", null);
            var _scrolllock = new StatusItem(Key.CharMask, "Scroll", null);

            var _statusBar = new StatusBar()
            {
                Visible = true,
            };

            _statusBar.Items = new StatusItem[] {
                _capslock,
                _numlock,
                _scrolllock,
                new StatusItem(Key.Q | Key.CtrlMask, "~CTRL-Q~ Quit", () => {
      //              if (_runningScenario is null){
						//// This causes GetScenarioToRun to return null
						//_runningScenario = null;
                        Application.RequestStop();
      //              } else {
      //                  _runningScenario.RequestStop();
      //              }
                }),
                new StatusItem(Key.F10, "~F10~ Hide/Show Status Bar", () => {
                    _statusBar.Visible = !_statusBar.Visible;
                    //_leftPane.Height = Dim.Fill(_statusBar.Visible ? 1 : 0);
                    //_bottomRightPane.Height = Dim.Fill(_statusBar.Visible ? 1 : 0);
                    this.Height = Dim.Fill(_statusBar.Visible ? 1 : 0);
                    //_top.LayoutSubviews();
                    //_top.SetChildNeedsDisplay();
                    this.LayoutSubviews();
                    this.SetChildNeedsDisplay();
                }),
            };

            //SetColorScheme();
            //var _top = Application.Top;
            this.RemoveAll();

            //_top.KeyDown += KeyDownHandler;
            //view.Add(menu);
            view.Add(_leftPane);
            view.Add(_topRightPane);
            view.Add(_bottomRightPane);
            //view.Add(_statusBar);
            Application.Top.Add(menu, _statusBar);
            //((TelegramTerminalClientWindow)view).StatusBar = _statusBar;
            //this.StatusBar = _statusBar;
            //_top.Loaded += () =>
            //{
            //if (_runningScenario != null)
            //{
            //    _runningScenario = null;
            //}
            //Console.WriteLine("NOT IMPLEMENTED: _top.Loaded");
            //throw new NotImplementedException();
            //};

            //this.Add(_top);
            //Application.Run(_top);
            //return _runningScenario;
        }

        private View LoginView(View view)
        {
            var phone = new Label("Phone Number: ")
            {
                X = 3,
                Y = 2
            };

            var phoneText = new TextField()
            {
                X = Pos.Right(phone),
                Y = Pos.Top(phone),
                Width = 40
            };

            var phoneExplanation = new Label("This should be in the format of country code and a number.\ne.g. +355123456789 where 355 is the country code of Albania.")
            {
                X = Pos.Left(phone),
                Y = Pos.Top(phoneText) + 1,
                ColorScheme = _colorScheme
            };
            phoneExplanation.ColorScheme.Normal = Terminal.Gui.Attribute.Make(Color.White, Color.Black);

            var buttonPositionY = Pos.Top(phoneExplanation) + 4;

            var btnLogin = new Button("Sign in")
            {
                X = Pos.Center() - 15,
                Y = buttonPositionY,
                IsDefault = true
            };
            btnLogin.Clicked += () => VerifyNumberView(phoneText.Text.ToString(), view);

            var btnExit = new Button("Exit")
            {
                X = Pos.Center() + 1,
                Y = buttonPositionY
            };
            btnExit.Clicked += () => Quit();

            var rememberMe = new CheckBox("Remember me")
            {
                X = Pos.Left(phone),
                Y = Pos.Top(phoneExplanation) + 2
                //ColorScheme = _colorScheme
            };
            //rememberMe.ColorScheme.Normal = Terminal.Gui.Attribute.Make(Color.BrightYellow, Color.Red); ;

            this.RemoveAll();
            view.Add(
                phone, phoneText, phoneExplanation, rememberMe,
                //new RadioGroup(3, 8, new[] { "_Personal", "_Company" }),
                btnLogin,
                btnExit//,
                       //new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar")
            );

            return view;
        }

        //private Pos Max(Pos i1, Pos i2)
        //{
        //    return i1.X > i2.X ? i1 : i2;
        //}
        private async void Login(string phoneNumber, string code, string hash)
        {
            try
            {
                var user = await _client.MakeAuthAsync(phoneNumber, hash, code);
                MainView(this);
            }
            catch (Exception ex)
            {
                int x = 5;
            }
        }

        private async Task<string> GetLoginCode(string phoneNumber)
        {
            string hash = null;
            try
            {
                //if (_client.Session == null) //TODO: Do we need this check? Should it be changed?
                {
                    hash = await _client.SendCodeRequestAsync(phoneNumber);
                    //await TLConnect(client);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message); //TODO: Hanle this errors - AUTH_RESTART, PHONE_NUMBER_INVALID
                //hash = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //hash = null;
            }

            return hash;
        }
        #endregion Help Methods
    }
}