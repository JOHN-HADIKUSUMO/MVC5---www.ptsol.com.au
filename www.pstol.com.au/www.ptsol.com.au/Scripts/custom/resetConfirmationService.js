var resetConfirmationService = appModule.factory('resetConfirmationService', ['$http', function ($http) {
    var resetConfirmationService = {
        postConfirmation: function (dataConfirmation) {
            return $http.post('/REST/REGISTRATION/NEW-PASSWORD/CONFIRM', dataConfirmation);
        }
    };

    return resetConfirmationService;
}]);