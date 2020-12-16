using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace TelegramClient
{
    /// <summary>
    /// The top-level window to show
    /// </summary>
    internal class TelegramTerminalClientWindow : Window
    {
        #region Fields Members
        private bool _loggedIn = false;
        private View _activeView;
        private ColorScheme _colorScheme;
        #endregion Fields Members

        #region Properties
        #endregion Properties

        #region Constructors
        public TelegramTerminalClientWindow()
        {
            var top = Application.Top;
            top.Add(this);

            _colorScheme = new ColorScheme();

            this.Title = "Telegram Client";
            this.X = 0;
            this.Y = 1; // Leave one row for the toplevel menu

            // By using Dim.Fill(), it will automatically resize without manual intervention
            this.Width = Dim.Fill();
            this.Height = Dim.Fill();

            // Creates a menubar, the item "New" has a help menu.
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new[] {
                    new MenuItem ("_New", "Creates new file", NewFile),
                    new MenuItem ("_Close", "", Close),
                    new MenuItem ("_Quit", "", () => { if (Quit ()) this.Running = false; }) //top.Running => this.Running
                }),
                new MenuBarItem ("_Edit", new[] {
                    new MenuItem ("_Copy", "", null),
                    new MenuItem ("C_ut", "", null),
                    new MenuItem ("_Paste", "", null)
                })
            });
            top.Add(menu);

            // Add some controls
            if (_loggedIn)
            {
                _activeView = new FrameView();
                MainView(_activeView);
            }
            else
            {
                _activeView = new Dialog("Login");
                LoginView(_activeView);
            }

            this.Add(_activeView);
        }
        #endregion Constructors

        #region Events/Actions
        private void VerifyNumberView(string number)
        {
            //TODO: Add a number verification
            var phoneNumber = new Label($"Please make sure your phone number is {number}")
            {
                X = Pos.Center(),
                Y = Pos.Percent(40)
            };

            var btnApprove = new Button("Approve")
            {
                X = Pos.Center() - 15,
                Y = Pos.Percent(50)
            };
            btnApprove.Clicked += () => LoginCodeView();

            var btnFix = new Button("Fix")
            {
                X = Pos.Center() + 1,
                Y = Pos.Percent(50)
            };
            btnFix.Clicked += () => LoginView(this);

            this.RemoveAll();
            this.Add(phoneNumber, btnApprove, btnFix);
            Login();
        }

        private void LoginCodeView()
        {
            var loginCode = new Label("Password: ")
            {
                X = Pos.Center(),
                Y = Pos.Percent(50)
            };

            var loginCodeText = new TextField("")
            {
                Secret = false,
                X = Pos.Right(loginCode) + 1,
                Y = Pos.Top(loginCode),
                Width = 7
            };

            var btnLogin = new Button("Log in")
            {
                X = Pos.Center(),
                Y = Pos.Percent(50)
            };
            btnLogin.Clicked += () => Login();

            this.RemoveAll();
            this.Add(loginCode, loginCodeText, btnLogin);
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
            throw new System.NotImplementedException();
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
            //Application.UseSystemConsole = _useSystemConsole;
            //Application.Init();

            // Set this here because not initialized until driver is loaded
            //_baseColorScheme = Colors.Base;

            StringBuilder aboutMessage = new StringBuilder();
            aboutMessage.AppendLine("UI Catalog is a comprehensive sample library for Terminal.Gui");
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

            var _menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new [] {
                    new MenuItem ("_Quit", "", () => Application.RequestStop(), null, null, Key.Q | Key.CtrlMask)
                }),
                //new MenuBarItem ("_Color Scheme", CreateColorSchemeMenuItems()),
                //new MenuBarItem ("Diag_nostics", CreateDiagnosticMenuItems()),
                new MenuBarItem ("_Help", new MenuItem [] {
                    //new MenuItem ("_gui.cs API Overview", "", () => OpenUrl ("https://migueldeicaza.github.io/gui.cs/articles/overview.html"), null, null, Key.F1),
                    //new MenuItem ("gui.cs _README", "", () => OpenUrl ("https://github.com/migueldeicaza/gui.cs"), null, null, Key.F2),
                    new MenuItem ("_About...", "About this app", () =>  MessageBox.Query ("About UI Catalog", aboutMessage.ToString(), "_Ok"), null, null, Key.CtrlMask | Key.A),
                })
            });

            var _leftPane = new FrameView("Categories")
            {
                X = 0,
                Y = 1, // for menu
                Width = 25,
                Height = Dim.Fill(1),
                CanFocus = false,
                Shortcut = Key.CtrlMask | Key.C
            };
            _leftPane.Title = $"{_leftPane.Title} ({_leftPane.ShortcutTag})";
            _leftPane.ShortcutAction = () => _leftPane.SetFocus();

            var _rightPane = new FrameView("Scenarios")
            {
                X = 25,
                Y = 1, // for menu
                Width = Dim.Fill(),
                Height = Dim.Fill(1),
                CanFocus = true,
                Shortcut = Key.CtrlMask | Key.S
            };
            _rightPane.Title = $"{_rightPane.Title} ({_rightPane.ShortcutTag})";
            _rightPane.ShortcutAction = () => _rightPane.SetFocus();

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
                _rightPane.SetFocus();
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
            _rightPane.Add(_scenarioListView);

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
      //                  Application.RequestStop();
      //              } else {
      //                  _runningScenario.RequestStop();
      //              }
      throw new NotImplementedException();
                }),
                new StatusItem(Key.F10, "~F10~ Hide/Show Status Bar", () => {
                    _statusBar.Visible = !_statusBar.Visible;
                    _leftPane.Height = Dim.Fill(_statusBar.Visible ? 1 : 0);
                    _rightPane.Height = Dim.Fill(_statusBar.Visible ? 1 : 0);
                    //_top.LayoutSubviews();
                    //_top.SetChildNeedsDisplay();
                }),
            };

            //SetColorScheme();
            //var _top = Application.Top;
            var _top = view;
            _top.KeyDown += KeyDownHandler;
            _top.Add(_menu);
            _top.Add(_leftPane);
            _top.Add(_rightPane);
            _top.Add(_statusBar);
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

        private void LoginView(View view)
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
                Y = buttonPositionY
            };
            btnLogin.Clicked += () => VerifyNumberView(phoneText.Text.ToString());

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
                btnExit,
                new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar")
            );
        }

        //private Pos Max(Pos i1, Pos i2)
        //{
        //    return i1.X > i2.X ? i1 : i2;
        //}


        private async void Login()
        {
            try
            {
                var apiId = ;
                var apiHash = "";
                Console.WriteLine("Hello World!");
                //var session = new Session
                //{
                //    AuthKey = ,
                //    Id = ,
                //    LastMessageId = ,
                //    Salt = ,
                //    Sequence = ,
                //    SessionExpires = ,
                //    TimeOffset = ,
                //    TLUser = 
                //}
                var client = new TLSharp.Core.TelegramClient(apiId, apiHash);
                await client.ConnectAsync();
                if (client.Session == null)
                {
                    await TLConnect(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task TLConnect(TLSharp.Core.TelegramClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var hash = await client.SendCodeRequestAsync("+");
            var code = ""; // you can change code in debugger //TODO: Cahnge this to Console.ReadLine

            var user = await client.MakeAuthAsync("+", hash, code);
        }
        #endregion Help Methods
    }
}