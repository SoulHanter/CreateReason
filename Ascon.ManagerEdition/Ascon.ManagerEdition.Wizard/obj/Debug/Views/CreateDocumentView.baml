   M S B A M L   `   `   `  ����Z  VAscon.ManagerEdition.Wizard.ext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Ascon.ManagerEdition.Wizard.Views!Ascon.ManagerEdition.Wizard.Views  5      R NAscon.Pilot.Theme, Version=17.0.22.19582, Culture=neutral, PublicKeyToken=null\@clr-namespace:Ascon.Pilot.Theme.Tools;assembly=Ascon.Pilot.ThemeAscon.Pilot.Theme.Tools 5      P:clr-namespace:Ascon.Pilot.Theme;assembly=Ascon.Pilot.ThemeAscon.Pilot.Theme 5      :    4Ascon.ManagerEdition.Wizard.Views.CreateDocumentView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:Ascon.ManagerEdition.Wizard.Views     5      Rtools@clr-namespace:Ascon.Pilot.Theme.Tools;assembly=Ascon.Pilot.Theme     5      Ltheme:clr-namespace:Ascon.Pilot.Theme;assembly=Ascon.Pilot.Theme     5      $	��170��5
      $	��420��6     � Title$6  0Создание нового документа��6'    � Topmost .  � WindowStartupLocation$ CenterScreen=�6X    � 	Resources 5       �� MergedDictionaries6  0Ascon.Pilot.Theme.Tools.SharedResourceDictionary   Source�� 5   
    5        5      $? 9/Ascon.Pilot.Theme;component/Themes/StandardControls.xaml?�61     5      $N H/Ascon.Pilot.Theme;component/Themes/sharedresources/sharedresources.xaml?�61     5      $I C/Ascon.Pilot.Theme;component/Themes/standardcontrols/textblock.xaml?�61   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5    
   �� $��15(�6   $��5q�6#   $
��Left=�6.   $��Тип документа��5!      $��Center=�6)   ��5"      �� *)  #Ascon.Pilot.Theme.StyleKeyExtension  *DialogTitleStyle++�� 5$   
   $��15(�6   $��1��6#   $��5q�60   $
��Left=�6;   $/��)Обозначение документа��5%      $��Center=�61   ��5&      �� *  *DialogTitleStyle++�� 5(   
   -   5        $
��Type��5(      $��0��6   $��1��6,   $��5q�6<   ��5)      �� *Types+ �� SelectedValue 5*      �� *Type+	 �� UpdateSourceTrigger$	 PropertyChanged=�5+      �� 5-   
   $��15(�6   $��5q�6!   $��Stretch=�6,   $��1��5.      $��1��6   ��5-   K   �� *Name+
 �� Mode$
 TwoWay=�$	 PropertyChanged=�5.      �� 50   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��1��6;   $��5q�6L   .+�51      �� $��5q�6   $
��Ок��6    $	��100��6-   ��69   �� *Ok+�� 52      $��5q�6   $��Отмена��6    $	��100��61   ��6=   �� *Cancel+53      55      56      