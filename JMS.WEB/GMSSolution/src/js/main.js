
  $(document).ready(function(){
    $('.sidenav').sidenav();
  });

  
  
var elem = document.querySelector('.collapsible.expandable');
var instance = M.Collapsible.init(elem, {
  accordion: false
});