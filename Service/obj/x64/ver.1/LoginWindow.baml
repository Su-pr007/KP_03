   M S B A M L   `   `   `  ���� B  >Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ServiceService      Service.LoginWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:Service     -     � Icon$  ico.ico�� � Title$ 
ServiceApp��$	��300��$	��350�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions ��  � ColumnDefinitions �� .+��� -    ��Name$ 
LoginLabel��$��Логин:��$
��Left=�$��	10,63,0,0q�$	��Top=�$��38��$��84��$��22(��� -    ��Name$ LoginTextBox��$
��Left=�$��35��$��	99,63,0,0q�$	��Top=�$	��226��$��22(��� -   $ PasswordLabel��$��Пароль:��$
��Left=�$��
10,104,0,0q�$	��Top=�$��38��$��84��$��22(�G� -    G�Name$ PasswordBox��$
��Left=�$��35��$��
99,104,0,0q�$	��Top=�$	��226��$��22(��� -   	 ��Name$	 EnterButton��$��
Войти��$
��Left=�$��41��$��
99,147,0,0q�$	��Top=�$	��146��$��22(����	#FFDDDDDD�� -   $	 GuestButton��$��Я клиент��$��Right=�$��34��$��	0,0,10,10q�$��Bottom=�$	��125��$��22(��� $��
ServiceApp��$
��Left=�$��
113,19,0,0q�$	��Top=�$��22(��� -   $	 ConnSettingWindowButton��$-��'Настройка соединения��$
��Left=�$��	10,0,0,10q�$��Bottom=�$
��10,1q�