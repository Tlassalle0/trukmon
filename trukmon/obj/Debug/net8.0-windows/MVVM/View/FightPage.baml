   M S B A M L   `   `   `  ����B  >trukmon, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:trukmon.MVVM.Viewtrukmon.MVVM.View  5      !    trukmon.MVVM.View.FightPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:trukmon.MVVM.View     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��2��6   $��Center=�6)   .+�5      �� $��Trukmon Battle��6   $��28(�6.   $
��Bold"�6<   
��� ���$��Center=�5   *   $��0,20,0,0q�6G   �� 5      $��18(�6/   $��SemiBold"�6=   
��� ���$��42��6e   ��6   �� *Score+6e   5      �� 5   
   $��0��6   $��Center=�6%   $��Center=�6B   $��20q�5      $	��300��6   ���#404040.+�5      �� $��Your Monster��6   $��20(�6,   $
��Bold"�6:   
������� �� TextAlignment$ Center=�5      $��0,0,0,10q�6'   �� 5      $��18(�6<   $
��Bold"�6J   
������$ Center=�5   (   ��5      �� *PlayerMonster.Name+5   (   �� 5      $��Health:��6   $��14(�6'   $��SemiBold"�65   
�������$��0,5,0,0q�5      � 5      $��20��5   >   
�������
��� � � � Value 5      �� *PlayerMonster.Health+ � Maximum 5      �� *PlayerMonster.MaxHealth+5       �� 5#      .+�5$      �� $��Your Spells:��6   $��18(�60   $��SemiBold"�6>   $��0,10,0,5q�6T   �� 5&      ��6   �� *PlayerMonster.Spells+��5'      �� 5(      .R�5)      �� $	��120��6>   $��5q�6J   ��6%   �� *Name+��5*      �� *DataContext.CastSpellCommand+ �� RelativeSource ��  �� AncestorType UserControl��5+      ��  �� *Self+ �� Path$	 Tag�
 � Tag 5,      �� 5-      5/      50      51      �� 54   
   $��1��6   $��Center=�6%   $��Center=�6B   $��20q�55      $	��300��6!   ���#404040.+�56      �� $��Opponent��6   $��20(�6(   $
��Bold"�66   
�������$ Center=�57      $��0,0,0,10q�6/   �� 58      $��18(�6>   $
��Bold"�6L   
�������$ Center=�59   0   ��58      �� *OpponentMonster.Name+59   0   �� 5:      $��Health:��6   $��14(�6'   $��SemiBold"�65   
�������$��0,5,0,0q�5;      � 5<      $��20��5=   H   
�������
��� � � 5<      �� *OpponentMonster.Health+ 5=      �� *OpponentMonster.MaxHealth+5>      �� 5?      $��80��6   $��2��6'   $
��Auto=�6;   $��Disabled=�6^   .��5@      ��  �� 
IsReadOnly$
 True��60   "	 �� VerticalScrollBarVisibility$
	 Auto=�6B   
 �� AcceptsReturn$

 True��6e   $��10q�6z   $��Bottom=�5A      $��9(�6-   ���#333333
�������
������ ��5@      �� *Logs+5A   b   5B      5C      5G      5H      