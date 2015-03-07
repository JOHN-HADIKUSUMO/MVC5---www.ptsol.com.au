var profileService = appModule.factory('profileService', ['$http', function ($http) {
    var profileService = {
        postProfile: function (dataProfile) {
            return $http.post('/REST/REGISTRATION/PROFILE/UPDATE', dataProfile);
        }
    };

    return profileService;
}]);