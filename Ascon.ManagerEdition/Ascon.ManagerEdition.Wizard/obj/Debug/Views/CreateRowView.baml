   M S B A M L   `   `   `  ����Z  VAscon.ManagerEdition.Wizard.ext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullg8clr-namespace:Ascon.ManagerEdition.Wizard.Views.Template*Ascon.ManagerEdition.Wizard.Views.Template  5      R NAscon.Pilot.Theme, Version=17.0.22.19582, Culture=neutral, PublicKeyToken=null\@clr-namespace:Ascon.Pilot.Theme.Tools;assembly=Ascon.Pilot.ThemeAscon.Pilot.Theme.Tools 5      P:clr-namespace:Ascon.Pilot.Theme;assembly=Ascon.Pilot.ThemeAscon.Pilot.Theme 5      W0clr-namespace:Ascon.ManagerEdition.Wizard.Models"Ascon.ManagerEdition.Wizard.Models  5	      V RAscon.ManagerEdition.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�dclr-namespace:Ascon.ManagerEdition.Common.MVVMCommon.Converters;assembly=Ascon.ManagerEdition.Common1Ascon.ManagerEdition.Common.MVVMCommon.Converters 5
      ]3clr-namespace:Ascon.ManagerEdition.Wizard.Converter%Ascon.ManagerEdition.Wizard.Converter  5      �Yclr-namespace:Ascon.ManagerEdition.Common.MVVMCommon;assembly=Ascon.ManagerEdition.Common&Ascon.ManagerEdition.Common.MVVMCommon 5      5    /Ascon.ManagerEdition.Wizard.Views.CreateRowView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jlocal8clr-namespace:Ascon.ManagerEdition.Wizard.Views.Template     5      Rtools@clr-namespace:Ascon.Pilot.Theme.Tools;assembly=Ascon.Pilot.Theme     5      Ltheme:clr-namespace:Ascon.Pilot.Theme;assembly=Ascon.Pilot.Theme     5      Cmodels0clr-namespace:Ascon.ManagerEdition.Wizard.Models     5	      {
convertersdclr-namespace:Ascon.ManagerEdition.Common.MVVMCommon.Converters;assembly=Ascon.ManagerEdition.Common     5
      KuserConvert3clr-namespace:Ascon.ManagerEdition.Wizard.Converter     5      jenumYclr-namespace:Ascon.ManagerEdition.Common.MVVMCommon;assembly=Ascon.ManagerEdition.Common     5        � 	Resources  5       �� MergedDictionaries6  0Ascon.Pilot.Theme.Tools.SharedResourceDictionary   SourceQ  KAscon.ManagerEdition.Common.MVVMCommon.Converters.EnumToItemSourceConverter   EnumToItemSourceConverter9   3Ascon.ManagerEdition.Wizard.Converter.ConvertToUser  ConvertToUser 	 Image �� Property �� Value�� 5   
    5        5      $? 9/Ascon.Pilot.Theme;component/Themes/StandardControls.xaml?�61     5      $N H/Ascon.Pilot.Theme;component/Themes/sharedresources/sharedresources.xaml?�61     5      $I C/Ascon.Pilot.Theme;component/Themes/standardcontrols/textblock.xaml?�61   %�   &	        &	      &	 &       5      63     5      6(   �� 5      .�5      ��  � ��$ 20��61   �� 5       � ��$ 20��60   �� 5       � ��$ Center=�6>   5      5      .��5      � -   5         �Name$ CreateOrEditView��5       � RowDefinitions 5   
   �� 5       $
��auto �6   �� 5!      $
��auto �6   �� 5"      $
��auto �6   �� 5#      �� 5$      �� 5%      $
��auto �6   �� 5&      �� 5'      $
��auto �6    � ColumnDefinitions 5)   
   �� 5*      $
��auto �6   �� 5+      .+�5/   
   �� $��15(�6   $��5q�6#   $
��Left=�6.   $��От кого��6J   $��Center=�6Y   $��0��50   Z   ��6   �� *)  #Ascon.Pilot.Theme.StyleKeyExtension  *DialogLabeledTextBlockStyle++6Z   �� 51   
   $��15(�6   $��5q�6#   $
��Left=�6.   $��Кому��6J   $��Center=�6V   $��1��52   Z   ��6   �� *  *DialogLabeledTextBlockStyle++6Z   �� 53   
   $��15(�6   $��5q�6#   $
��Left=�6.   $%��Краткое описание��6J   $��Center=�6b   $��2��54   Z   ��6   �� *  *DialogLabeledTextBlockStyle++6Z   �� 55   
   $��15(�6   $��5q�6#   $
��Left=�6.   $��Приложение��6J   $��Center=�6\   $��3��56   Z   ��6   �� *  *DialogLabeledTextBlockStyle++6Z   �� 57   
   $��15(�6   $��5q�6#   $
��Left=�6.   $'��!Документ редакции��6J   $��Center=�6c   $��4��58   Z   ��6   �� *  *DialogLabeledTextBlockStyle++6Z   �� 59   
   $��15(�6   $��5q�6#   $
��Left=�6.   $��Действие��6J   $��Center=�6Z   $��5��5:   Z   $y�	Collapsed=�6g   ��6   �� *  *DialogLabeledTextBlockStyle++6g   �� 5;   
   $��15(�6   $��5q�6#   $
��Left=�6.   $��Комментарий��6J   $��Center=�6]   $��6��5<   Z   ��6   �� *  *DialogLabeledTextBlockStyle++6Z   �� 5>   
   -   5        $��FromUser��5>      $��0��6#   $��1��60   $��5q�6@   ��5?      �� *GetFromUser+ �� SelectedValue 5@      �� *Remark.FromUser+	 �� UpdateSourceTrigger$	 PropertyChanged=�5A      �� 5C   
   -   5        $��ToUser��5C      $��1��6!   $��1��6.   $��5q�6>   ��5D      �� *	GetToUser+ 5E      �� *Remark.ToUser+$	 PropertyChanged=�5F      �� 5H   
   $��15(�6   $��5q�6!   $��Stretch=�6,   $��2��5I      $��1��6   ��5H   K   �� *Remark.Description+
 �� Mode$
 TwoWay=�$	 PropertyChanged=�5I      � 5K   
   $��3��6   $��1��6   $��5q�6,    5L      �� 5M      �� 5N      $
��auto �6#   .+�5Q      C   =Ascon.ManagerEdition.Wizard.Views.Template.SourceTemplateView  $��Stretch=�6'    � DataContext 5R      �� *Enlargements+$
 OneTime=�$	 PropertyChanged=��� 5S      $��1��6   $��5q�6%   $)��#Выбрать приложения��6L    � ToolTip$) #Выбрать приложения��6r   ��60   �� *	AddFile+.��5T      �� $F��@/Ascon.ManagerEdition.Wizard.ext;component/Icons/Add_File_16.png��6   #��[ 6b   5U      5V      � 5Y   
   $��4��6   $��1��6   $��5q�6,    5Z      �� 5[      �� 5\      $
��auto �6#   .+�5_        $��Stretch=�6'    5`      �� *	Documents+$
 OneTime=�$	 PropertyChanged=��� 5a      $��1��6   $��5q�6%   $8��2Выбрать документы редакции��6L   $8 2Выбрать документы редакции��6z   ��60   �� *	AddFile+.��5b      �� $F��@/Ascon.ManagerEdition.Wizard.ext;component/Icons/Add_File_16.png��6   #��[ 6b   5c      5d      �� 5h   
   -   5        $	��Act��5h      $��5��6   $��1��6+   $��5q�6;   $y�	Collapsed=�5i   7    J� IsHitTestVisible 6   �� *IsEdit+��5j      ��  �� Source @  :Ascon.ManagerEdition.Common.MVVMCommon.EnumToBindingSource  *M� *models:Acts++ 5k      �� *
Remark.Act+ �� 	Converter# [   �� ConverterParameter-   'Ascon.ManagerEdition.Wizard.Models.Acts"  $	 PropertyChanged=�5l      �� 5n   
   $��15(�6   $��5q�6!   $��Stretch=�6,   $��6��5o      $��1��6    �� TextWrapping$
 Wrap=�6/    �� AcceptsReturn . ��5n   K   �� *Remark.Commit+$
 TwoWay=�$	 PropertyChanged=�5o   C   � 5q   
   $��7��6   $��1��6    5r      �� 5s      �� 5t      $
��auto �6#   .+�5v      �� $��5q�6   $
��Ок��6    $��Right=�6D   $	��200��6`   ��6-   �� *Ok+6`   �� 5w      $��5q�6   $��Отмена��6    $��1��6M   $��Right=�6]   $	��200��6y   ��62   �� *Cancel+6y   5x      5y      5z      