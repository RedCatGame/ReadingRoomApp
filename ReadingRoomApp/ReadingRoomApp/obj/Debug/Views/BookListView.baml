   M S B A M L   `   `   `  ����I  EReadingRoomApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ReadingRoomApp.ViewsReadingRoomApp.Views  5      E'clr-namespace:ReadingRoomApp.ViewModelsReadingRoomApp.ViewModels  5      '    !ReadingRoomApp.Views.BookListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:ReadingRoomApp.Views     5      >
viewModels'clr-namespace:ReadingRoomApp.ViewModels     5        � 	Resources  5       �� DataType0   *ReadingRoomApp.ViewModels.AddBookViewModel&    ReadingRoomApp.Views.AddBookView1   +ReadingRoomApp.ViewModels.EditBookViewModel'   !ReadingRoomApp.Views.EditBookView4   .ReadingRoomApp.ViewModels.BookDetailsViewModel*   $ReadingRoomApp.Views.BookDetailsView��%�   (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      � .+�5   
   � $��10q�6   y�6   �� *IsListViewVisible+   BoolToVisibilityConverter �� 	Converter# [   � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $��Список книг��6   $��24(�5       $
��Bold"�5!      $��0,0,0,10q�5"      $��0��5#      &   System.Windows.Controls.DataGrid  5%      -   5        $��1��5%         AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�5*      $��1q�5+      ���#DDDDDD��5&      �� *Books+ �� SelectedItem 5'      �� *SelectedBook+   Columns 5.      0  *System.Windows.Controls.DataGridTextColumn  5/      ,	  &System.Windows.Controls.DataGridColumn	 	  Header$	 ID��6)   
 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$
 50
 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+6L     50      $	 Название��6)   $
 *
 6U    6;   �� *Title+6U     51      $	 
Автор��6)   $	
 150
 6\    68   �� *Author.FullName+6\     52      $	 Жанр��6)   $	
 120
 6V    67   �� *
Genre.Name+6V     53      $	 Год издания��6)   $	
 100
 6b    6>   �� *PublicationYear+6b   4  .System.Windows.Controls.DataGridCheckBoxColumn  54      $	 Доступна��6-   $
 80
 6_    6?   �� *IsAvailable+6_   56      �� 58      $��2��6    �� Orientation$ 
Horizontal=�59      $��Right=�5:      $��0,10,0,0q�5;      .+�5<      �� $��Добавить��6   $
��10,5q�5>      $��0,0,10,0q�5?      ���#2196F3
�������$��0q�5B      ��5=      �� *AddBookCommand+5B      �� 5C      $��Просмотр��6   $
��10,5q�5E      $��0,0,10,0q�5F      ���#00BCD4
�������$��0q�5J      ��5D      �� *ViewBookDetailsCommand+{�5G      �� *SelectedBook+  NullToBoolConverter# [ 5J      �� 5K      $ ��Редактировать��6   $
��10,5q�5M      $��0,0,10,0q�5N      ���#FFC107
�������$��0q�5R      ��5L      �� *EditBookCommand+{�5O      �� *SelectedBook+# [ 5R      �� 5S      $��Удалить��6   $
��10,5q�5U      ���#F44336
�������$��0q�5Y      ��5T      �� *DeleteBookCommand+{�5V      �� *SelectedBook+# [ 5Y      5Z      5[      �� 5]   
   ��6   �� *CurrentView+5^      5_      