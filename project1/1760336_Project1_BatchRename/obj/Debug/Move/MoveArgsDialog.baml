   M S B A M L   `   `   `  ����L  HBatch Rename v1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:_1760336_Project1_BatchRename.Move"_1760336_Project1_BatchRename.Move  5   	   7    1_1760336_Project1_BatchRename.Move.MoveArgsDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:_1760336_Project1_BatchRename.Move     5   	     � Title$  Move Setting��5   	   $��235.874��6   $��429.874��6/    � 
ResizeMode$ NoResize=�6?   .��5	      �� .+�5
   
   �� $
��Wrap=�6   $��13(�6(   $��54��66    �� Left$ 10��6B   
 �� Top$ 10��6S   $	��384��6c   ��This function is used to move a substring in a file / folder name to another position. To start with, you have to type in the following arguments:6  �� 5   
   $
��Wrap=�6   $��12(�6(   $��18��66   $ 10��6B   $ 69��6S   $	��162��6c   Start position of substring:6�   �� 5   
   -   5        $��startIndexTextBox��5      $	��217��6+   $	 177��67   $ 69��6I   6�   �� 5   
   $
��Wrap=�6   $��12(�6(   $��18��66   $ 10��6B   $ 92��6S   $	��162��6c   Length of substring:6�   �� 5   
   -   5        $��lengthTextBox��5      $	��217��6'   $	 177��63   $ 92��6E   6   �� 5   
   $
��Wrap=�6   $��12(�6(   $��18��66   $ 10��6B   $	 115��6S   $	��162��6d   New position of substring:6�   �� 5   
   -   5        $��insertIndexTextBox��5      $	��217��6,   $	 177��68   $	 115��6J   6�   �� 5   
   ���	#FFEDEDED$	��424��6+   $	 146��67   ���	#FFF8F8F8$��61��6_   6m   �� 5   
   -   5        $��
saveButton��5      $
��Save��6#   $	 219��6K   $	 155��6]   $��30��6n   $��88��6z   6�   �� 5   
   -   5        $��cancelButton��5       �� IsCancel . $��Cancel��66   $��30��6G   $��81��6S   $	 321��6^   $	 155��6p   6�   5      5      