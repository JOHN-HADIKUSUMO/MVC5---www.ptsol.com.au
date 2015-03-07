var changePwdOnBluredDirective = appModule.directive('changePwdOnBluredDirective', ['$http', function ($http) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function(scope, elem, attrs)
        {
            var p = elem;
            elem[0].addEventListener('blur',scope.test)
        }
    };
}]);