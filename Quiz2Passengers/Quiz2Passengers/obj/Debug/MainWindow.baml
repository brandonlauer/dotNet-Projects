   M S B A M L   `   `   `  ����J  FQuiz2Passengers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Quiz2PassengersQuiz2Passengers  5   	        Quiz2Passengers.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Quiz2Passengers     5   	     � Title$  
Passengers��5   	   $��400.546��6   $��583.174��6-   .��5	      � .+�5
   
   �� -   5        $��lvPassengers��5
   (   $��10,56,10,10q�6    �� SelectionMode$ Single=�6m    �� View 5      �� 5      .P�5      �� $
��Name��6%    �� Width$	 150��63    �� DisplayMemberBinding 6?   �� *Name+�� 5      $��Passport��6%   $	 100��67    6C   �� *
Passport+�� 5      $��Destination��6%   $	 150��6:    6F   �� *Destination+�� 5      $��Dep. date time��6%   $	 150��6=    6I   �� *DepartureDateTime+5       � 	Resources 5       �� 
TargetType �� Triggers �� Binding	 �� Value
 �� Property �� Value��%  '��       �� 5      " ��6    5      �� 5       6&   �� *HasDeparted+	 True6F   .a�5      �� 
 � ��
 �����6;   5      �� 5       6&   �� *HasDeparted+		 False6F   .a�5      �� 
 � ��
 �2͚�6;   5      5      5      5      �� 5    
   $��Search:��6   $
��Left=�6"   $��	10,10,0,0q�6=   $	��Top=�6P   �� 5!   
   -   5        $��tbSearch��5!      $
��Left=�6E   $��23��6`   $��	63,14,0,0q�6l    �� TextWrapping$
 Wrap=�6   $	��Top=�6�   $	��149��6�   �� 5"   
   -   5        $��btSort��5"      $��
Sort by...��64   $
��Left=�6I   $��
311,10,0,0q�6d   $	��Top=�6x   $	��108��6�   $��27��6�   �� 5#   
   -   5        $��btAdd��5#      $	��Add��62   $
��Left=�6@   $��
448,10,0,0q�6[   $	��Top=�6o   $	��108��6�   $��27��6�   5$      5%      