   M S B A M L   `   `   `  ����I  EReadingRoomApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:ReadingRoomApp.ViewModelsReadingRoomApp.ViewModels  5   	   ;"clr-namespace:ReadingRoomApp.ViewsReadingRoomApp.Views  5   	   %    ReadingRoomApp.Views.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >
viewModels'clr-namespace:ReadingRoomApp.ViewModels     5   	   4views"clr-namespace:ReadingRoomApp.Views     5   	     � Title$  Читальный зал��5	   	   $	��600��6   $	��800��6,    � 	Resources 5
       �� DataType1   +ReadingRoomApp.ViewModels.BookListViewModel'   !ReadingRoomApp.Views.BookListView3   -ReadingRoomApp.ViewModels.ReaderListViewModel)   #ReadingRoomApp.Views.ReaderListView.   (ReadingRoomApp.ViewModels.LoginViewModel$   ReadingRoomApp.Views.LoginView1   +ReadingRoomApp.ViewModels.RegisterViewModel'   !ReadingRoomApp.Views.RegisterView��%,  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5!   
   o� $��0��6   y�6   �� *
IsLoggedIn+   BoolToVisibilityConverter �� 	Converter# [  .@�5"      m� $��
Книги��6   .@�5#      m� $��Список книг��6    m� Command 60   �� *NavigateToBookListCommand+m� 5$      $!��Добавить книгу��6    63   �� *NavigateToAddBookCommand+y�5%      �� *
IsAuthor+# [  m� 5&      $��Мои книги��6    6.   �� *NavigateToMyBooksCommand+y�5'      �� *
IsAuthor+# [  5(      m� 5)      $��Читатели��6   y�6)   �� *	IsAdmin+# [  .@�5*      m� $%��Список читателей��6    65   �� *NavigateToReaderListCommand+m� 5+      $'��!Добавить читателя��6    66   �� *NavigateToAddReaderCommand+5,      m� 5-      $��Управление��6   y�6+   �� *	IsAdmin+# [  .@�5.      m� $��Авторы��6    6+   �� *NavigateToAuthorsCommand+m� 5/      $��
Жанры��6    6*   �� *NavigateToGenresCommand+m� 50      $��Пользователи��6    61   �� *NavigateToUsersCommand+51      m� 52      $��Профиль��6   .@�53      m� $��Мой профиль��6    60   �� *NavigateToProfileCommand+m� 54      $!��Моя библиотека��6    63   �� *NavigateToMyLibraryCommand+y�55      �� *
IsReader+# [  m� 56      $��
Выход��6    6*   �� *LogoutCommand+57      58      �� 5;   
   $��1��6   ��6&   �� *CurrentView+�� 5>   
   $��2��6   y�6!   �� *
IsLoggedIn+# [  .@�5?      �� .��5@      �� �� 5A      $ �Пользователь: ��6    �� 5B      �6   �� *CurrentUser.Username+ �� 5C      $� (��6    �� 5D      �6   �� *CurrentUser.Role+  UserRoleToStringConverter# [  �� 5E      $�)��6   5F      5G      5H      5I      5J      