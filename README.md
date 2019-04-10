# BlazorUIComponents
At attempt to make Blazor components that mimic UWP/Xamarin controls, follow MVVM (ViewModel nav) guidelines, and use the ReactiveUI framework

1. Navigation is half-finished.  Refreshes and direct links will not work.  They just make the app go back to the first page.  But everything else works.
2. Since I am trying to do Viewmodel-based navigation, using links '<a href=''></a>' are not going to work.  I couldn't use Bootstraps nice navigation bar since it depends on those links.  I had to use button instead.  Also, the `MainViewModel` doesn't default to a page... the menu unselected on first load.  
3. 
