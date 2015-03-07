var ngOnBlur = appModule.directive('ngOnBlur', ['$http', function ($http) {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            ngCallBack:'&'
        },
        link: function(scope, elem, attrs)
        {
            var p = elem;
            elem[0].addEventListener('blur',scope.ngCallBack )
        }
    };
}]);