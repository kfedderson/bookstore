   M S B A M L   `   `   `  ����D  @BookStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:BookStore	BookStore  5   	       BookStore.Items.AddItems   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:BookStore     5   	     � Title$  AddItems��5   	   $	��900��6   $
��1200��6'    � WindowStartupLocation$ CenterScreen=�64   .��5	      � ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      �  � Color$ 	#FFEAF0FF��6   � 5      # � ��6    � Offset$ 1Q�6g   5      .+�5   
   ]� $��	0,0,0,100q�6   .+�5      � $
��Left=�6   $	��250��6)   $	��Top=�66    � ColumnDefinitions 5      �� 5      $	��80* �6'   �� 5      $	��20* �6'   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��itemDataGrid��5      $
��Left=�5      $��10,0,0,0q�66   $	��Top=�6H   $	��220��6�      AutoGenerateColumns .  	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6.      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6:   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   ��  �� Path$ ItemCode�  5      $
 	Book Name��6-   $	 100 6@    6L   �� $ Title�  5      $
 Author Name��6-   $	 100 6B    6N   �� $ Author�  5      $
 Price��6-   $	 100 6<    6H   �� $ Price�5      �� 5       $��0q�6   $��1��6(   .+�5!      �� -   5         ��Name$ btnAddNewItem��5!      $��Add Item��64   $��20q�6G   6S   �� 5"      -   5        $ btnEditItem��5"      $��
Save Edits��62   $��Center=�6G   $	��Top=�6k   $	��Top=�6�   $��20q�6�   6�   �� 5#      -   5        $ btnDeleteItem��5#      $��Delete Item��64   $��20q�6J   6V   �� 5$      -   5        $ btnClearSelection��5$      $��Clear��68   $��20q�6H   6T   5'      5*      5+      �� 5,   
   -   5         ��Name$ 
gbEditItem��5,      $��GroupBox��6'   $
��Left=�69   $��10,260,10,0q�6T   $	��Top=�6i   $	��575��6�   $	��100��6�   .��5-      � -   5         �Name$ editGrid��5-      $
��Left=�6%   $��87��6@   $��10,0,0,0q�6L   $	��Top=�6^   $	��550��6v    5.      �� 5/      $��6* �6'   �� 50      $
��269* �6'   .+�52      �� -   5         ��Name$ lblItemCode��52      $��	Item Code��6-   $��1��6B   $
��Left=�6R   $��	10,10,0,0q�6m   $	��Top=�6�   $��26��6�   $��69��6�   �� 53      -	   5        $ lblBookName��53      $��	Book Name��6-   $��1��6A   $
��Left=�6Q   $��	84,10,0,0q�6l   $	��Top=�6   $��26��6�   $��82��6�   �� 54      -
   5        $ 	lblAuthor��54      $��Author��6+   $��1��6<   $
��Left=�6L   $��
296,10,0,0q�6g   $	��Top=�6{   $��26��6�   $��80��6�   �� 55      -   5        $ lblPrice��55      $��Price��6*   $��1��6:   $
��Left=�6J   $��
478,10,0,0q�6e   $	��Top=�6y   $��26��6�   $��36��6�    J� RenderTransformOrigin$ 2.556,0.423&�6�   �� 56      -   5         ��Name$ txtbxItemCode��56      $
��Left=�61   $��28��6L   $��	10,36,0,0q�6X    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   $��35��6�   $��1��6�   �� 57      -   5        $ txtbxBookName��57      $
��Left=�61   $��23��6L   $��	84,41,0,0q�6X   $
 Wrap=�6k   $	��Top=�6   $	��186��6�   $��1��6�   �� 58      -   5        $ txtbxAuthor��58      $
��Left=�6/   $��23��6J   $��
296,41,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $	��159��6�   $��1��6�   �� 59      -   5        $ 
txtbxPrice��59      $
��Left=�6.   $��23��6I   $��
478,41,0,0q�6U   $
 Wrap=�6i   $	��Top=�6}   $��37��6�   $��1��6�   $ 0.321,0.217&�6�   5:      5=      5>      5?      