   M S B A M L   `   `   `  ����I  EReadingRoomApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ReadingRoomApp.ViewsReadingRoomApp.Views  5      (    "ReadingRoomApp.Views.AddReaderView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:ReadingRoomApp.Views     5      .��5
      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $8��2Добавление нового читателя��6   $��24(�5      $
��Bold"�5      $��0,0,0,20q�5      $��0��5      �� 5   
   $��1��6   $
��Auto=�6$   .��5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'     5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   �� 5!      $
��Auto �6$   .+�5%      �� $��Имя:��6   $��0��5&      $��0��5'      $��Center=�5(      $��	0,0,10,15q�5)      �� 5*      $��0��5+      $��1��5,      $��0,0,0,15q�5-      $��5q�5.      ��5*      �� *NewReader.FirstName+ �� UpdateSourceTrigger$ PropertyChanged=�5.      �� 51      $��Фамилия:��6   $��1��52      $��0��53      $��Center=�54      $��	0,0,10,15q�55      �� 56      $��1��57      $��1��58      $��0,0,0,15q�59      $��5q�5:      ��56      �� *NewReader.LastName+$ PropertyChanged=�5:      �� 5=      $��Email:��6   $��2��5>      $��0��5?      $��Center=�5@      $��	0,0,10,15q�5A      �� 5B      $��2��5C      $��1��5D      $��0,0,0,15q�5E      $��5q�5F      ��5B      �� *NewReader.Email+$ PropertyChanged=�5F      �� 5I      $��Телефон:��6   $��3��5J      $��0��5K      $��Center=�5L      $��	0,0,10,15q�5M      �� 5N      $��3��5O      $��1��5P      $��0,0,0,15q�5Q      $��5q�5R      ��5N      �� *NewReader.PhoneNumber+$ PropertyChanged=�5R      5S      5T      �� 5V   
   $��2��6    �� Orientation$ 
Horizontal=�5W      $��Right=�5X      $��0,20,0,0q�5Y      .+�5Z      �� $��Отмена��6   $��15,10q�5\      $��0,0,10,0q�5]      ���#E0E0E0$��0q�5_      ��5[      �� *CancelCommand+5_      �� 5`      $��Сохранить��6   $��15,10q�5b      ���#2196F3
�������$��0q�5e      ��5a      �� *SaveCommand+5e      5f      5g      5h      