   M S B A M L   `   `   `  ���� B  >Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ServiceService      Service.CreateOrderWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:Service     -     �Name$  CreateOrderWindow1�� � Title$# Создание заказа��$	��300��$	��380��$	��356��$	��264��$��14(� � Icon$ ico.ico�� � WindowStartupLocation$ CenterScreen=�.��� .+��� $#��Ф.И.О. заказчика:��$
��Left=�$��	10,30,0,0q�$	��Top=��� -    ��Name$ OrdererNameTB��$��Right=�$��	0,34,10,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��150��$��21���� $"��Тип устройства:��$
��Left=�$��	10,64,0,0q�$	��Top=��� -   $ ModelTypeTB��$��Right=�$��	0,68,10,0q�$
 Wrap=�$	��Top=�$	��150��$��21���� $(��"Модель устройства:��$
��Left=�$��	10,98,0,0q�$	��Top=��� -   $ ModelNameTB��$��Right=�$��
0,102,10,0q�$
 Wrap=�$	��Top=�$	��150��$��21���� $2��,Описание неисправности:��$
��Left=�$��
10,133,0,0q�$	��Top=��� -   $ FaultDescTB��$��Right=�$��
0,136,10,0q�$
 Wrap=�$	��Top=�$	��150��$��21���� $"��Серийный номер:��$
��Left=�$��
10,166,0,0q�$	��Top=��� -   $ SerialNumberTB��$��Right=�$��
0,170,10,0q�$
 Wrap=�$	��Top=�$	��150��$��21���� -    ��Name$ SaveOrderButton��$��Сохранить��$��Right=�$��	0,0,10,10q�$��Bottom=�$��75��$��1��$��23���� -   $ CancelOrderButton��$��Отмена��$
��Left=�$��	10,0,0,10q�$��Bottom=�$��75��$��23��