   M S B A M L   `   `   `  ����I  EReadingRoomApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ReadingRoomApp.ViewsReadingRoomApp.Views  5      E'clr-namespace:ReadingRoomApp.ViewModelsReadingRoomApp.ViewModels  5      )    #ReadingRoomApp.Views.ReaderListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:ReadingRoomApp.Views     5      >
viewModels'clr-namespace:ReadingRoomApp.ViewModels     5        � 	Resources  5       �� DataType2   ,ReadingRoomApp.ViewModels.AddReaderViewModel(   "ReadingRoomApp.Views.AddReaderView6   0ReadingRoomApp.ViewModels.ReaderDetailsViewModel,   &ReadingRoomApp.Views.ReaderDetailsView��%�   (��       5      *, +)(�� 2     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      � .+�5   
   � $��10q�6   y�6   �� *IsListViewVisible+   BoolToVisibilityConverter �� 	Converter# [   � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $%��Список читателей��6   $��24(�5      $
��Bold"�5      $��0,0,0,10q�5      $��0��5       &   System.Windows.Controls.DataGrid  5"      -   5        $��1��5"         AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�5'      $��1q�5(      ���#DDDDDD��5#      �� *	Readers+ �� SelectedItem 5$      �� *SelectedReader+   Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 50 6L   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 65   �� *Id+6L     5-      $	 Имя��6)   $	
 120 6T    66   �� *	FirstName+6T     5.      $	 Фамилия��6)   $	
 150 6W    6:   �� *
LastName+6W     5/      $	 Email��6)   $
 * 6R    68   �� *Email+6R     50      $	 Телефон��6)   $	
 120 6Z    6:   �� *PhoneNumber+6Z     51      $	 Книг на руках��6)   $	
 100 6h    6@   �� *BorrowedBooks.Count+6h   53      �� 55      $��2��6    �� Orientation$ 
Horizontal=�56      $��Right=�57      $��0,10,0,0q�58      .+�59      �� $��Добавить��6   $
��10,5q�5;      $��0,0,10,0q�5<      ���#2196F3
�������$��0q�5?      ��5:      �� *AddReaderCommand+5?      �� 5@      $��Просмотр��6   $
��10,5q�5B      $��0,0,10,0q�5C      ���#00BCD4
�������$��0q�5G      ��5A      �� *ViewReaderDetailsCommand+{�5D      �� *SelectedReader+  NullToBoolConverter# [ 5G      �� 5H      $ ��Редактировать��6   $
��10,5q�5J      $��0,0,10,0q�5K      ���#FFC107
�������$��0q�5O      ��5I      �� *EditReaderCommand+{�5L      �� *SelectedReader+# [ 5O      �� 5P      $��Удалить��6   $
��10,5q�5R      ���#F44336
�������$��0q�5V      ��5Q      �� *DeleteReaderCommand+{�5S      �� *SelectedReader+# [ 5V      5W      5X      �� 5Z   
   ��6   �� *CurrentView+5[      5\      