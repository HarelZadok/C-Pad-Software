   M S B A M L   `   `   `  ????I  EC-Pad Software, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:C_Pad_SoftwareC_Pad_Software  5   	   /clr-namespace:C_Pad_SoftwareC_Pad_Software  5   	       C_Pad_Software.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +ucclr-namespace:C_Pad_Software     5   	   .localclr-namespace:C_Pad_Software     5   	   
???????  ? Title$  CPad Driver Software??5
   	   $	??500??5   	   $	??800??5   	   $	??500??5   	   $	??800??5   	    ? 	Resources 5         NotifierContextMenu ?? 	Placement m? IsCheckable m? 	IsChecked??%  &	        ?? 5   
   $??TrayMenu??5      $ 
MousePoint=?5      .@?5      m? -   5        $
??Open??5      6%   m? 5      -   5        $??Close??5      6&   m? 5      -   5        $??Always on top??5       .  .  6S   5      5      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??200 ?6   6-   ?? 5      $??* ?6   6+   ?? 5      $??* ?6   6+    ? RowDefinitions 5    
   ?? 5!      $??60 ?6   6*   ?? 5"      $??2* ?6   6*   ?? 5#      $??2* ?6   6*   ?? 5$      $??2* ?6   6*   .+?5&   
   ?? -   5        $??Header??5'      $??0??5(      $??3??5)      $??0??5*      $??Center=?5+      $??10q?5,      $	??Top=?5-      $??25(?5.      $
??Bold"?5/      
???   ?CPad52      ?? 53   
   $??1??54      $??0??55      $??3??56      $??Stretch=?57      $??Stretch=?58      
???????
???   ?$??0,2,2,0q?5;       ?? CornerRadius$ 20,0,0,0??5<      ._?5=      ?  5>      ?? 5?      $??* ?6$   61   ?? 5@      $??* ?6$   61   ?? 5A      $??* ?6$   61   ?? 5B      $??* ?6$   61   ?? 5C      $??* ?6$   61   ?? 5D      $??* ?6$   61   .+?5F      ?? -   5        $??1??5G      
?????????	#00FFFFFF$??0q?5I      $
??Bold"?5J      $??20(?5K      $??Center=?5L      $??Center=?5M      .??5O      ?? Macro Creation60   5P      ?? 5Q      -   5        $??3??5R      
?????????	#00FFFFFF$??0q?5U      $
??Bold"?5V      $??20(?5W      $??Center=?5X      $??Center=?5Y      .??5[      ?? 
Settings6*   5\      5]      5^      ?? 5_   
   $??1??6   $??1??5`      $??2??5a      $??3??5b      
???   ?$??0,2,0,0q?5d      $ 0,20,0,0??5e      ._?5f      ?? -   5        $??SettingsContent??5g      $??	10,15,0,0q?5h      $??Stretch=?5i      $??Stretch=?5j      $y?	Collapsed=?5k      .+?5l      ?? $??20(?6   $
??Bold"?5m      $??0,0,0,10q?5n      Button tick interval5p      ?? 5q       ?? Orientation$ 
Horizontal=?6   .+?5r      ?? -   5        $??TbTickInterval??5u      $??50??5r      $
??10,0q?5s      $??Center=?5t      .??5v   B   60005x      ?? 5y      CAμs/microseconds (low value may impact your computer performance)6b   5z      ?? 5{      $??20(?6   $
??Bold"?5|      $??	0,20,0,10q?5}      Always on top5      ?? 5?      -	   5        $??CbAlwaysOnTop??5?      $
??10,0q?5?      5?   :   ?? 5?      $??20(?6   $
??Bold"?5?      $??	0,20,0,10q?5?      Quit5?      ?? 5?      -
   5        $
??10,0q?5?      $??60??5?      $??25??5?      $
??Left=?5?      $??0q?5?      
??????
???????$
??Bold"?5?      .??5?   /   Quit65   5?      5?      ?? 5?   
   -   5        $??MacroCreationContent??5?      $??1??5?      $??1??5?      $??2??5?      $??3??5?      $??Hidden=?5?      $y?Visible=?5?      $??	10,15,0,0q?5?      .??5?      ?? $??Stretch=?5?      $??Stretch=?5?      .+?5?      ? $	??Top=?6   $??Stretch=?5?      .+?5?      ? $??0,2,0,0q?5?      $	??Top=?5?      $??Stretch=?5?      .+?5?      ?? -   5        .@?5?      ?? 	 ?? 
IsSelected	 . .??6=   
Button 06G   ?? 5?      .??6+   
Button 165   ?? 5?      .??6+   
Button 265   ?? 5?      .??6+   
Button 365   5?      ?? 5?      -   5        $??	CbRunType??5?      $
??10,0q?5?      $??85??5?      .@?5?      ?? 	 . .??6=   Single6E   ?? 5?      .??6+   Loop61   ?? 5?      .??6+   
While held67   5?      ?? 5?      -   5        $??0,0,10,0q?5?      $??85??5?      .@?5?      ?? 	 . .??6=   Key6B   ?? 5?      .??6+   File61   ?? 5?      .??6+   Delay62   ?? 5?      .??6+   Text61   5?      ?? 5?      -   5        ???	#00FFFFFF$??20??5?   !   $??20??5?   !   $??0q?5?   !   $??0,0,0,0q?5?   !   ??5?   "   ?? 5?   &   
 ?? 
TargetType"
 ??66   .R?5?   *   ?? $??img/Add.png??60   6G   5?   '   5?      ?? 5?      -   5        ???	#00FFFFFF$??20??5?   !   $??20??5?   !   $??0q?5?   !   $??10,0,0,0q?5?   !   ??5?   "   ?? 5?   &   "
 ??66   .R?5?   *   ?? $??img/Remove.png??60   6J   5?   '   5?      ?? 5?      -   5        $??CheckAll??5?   #   $??Center=?5?   #   $??10,0,0,0q?5?   #   5?      A? 5?      -   5        $??B0State??5?   "   
???  ??$??20,0,0,0q?5?   "   $??10??5?   "   $??10??5?   "   A? 5?      -   5        $??B1State??5?   "   
???  ??$??20,0,0,0q?5?   "   $??10??5?   "   $??10??5?   "   A? 5?      -   5        $??B2State??5?   "   
???  ??$??20,0,0,0q?5?   "   $??10??5?   "   $??10??5?   "   A? 5?      -   5        $??B3State??5?   "   
???  ??$??20,0,0,0q?5?   "   $??10??5?   "   $??10??5?   "   5?      ?? 5?      -   5        ???	#00FFFFFF$	??Top=?5?      $??Right=?5?      
??????$??0q?5?      $??16(?5?      $??0,0,10,0q?5?      $
??Bold"?5?      .??5?      ?? Apply5?      5?      5?      ?? 5?      .+?5      ? -   5        $??Panel??5       $
??Left=?6-   $	??Top=?6H   5     5     5     5     5     5     