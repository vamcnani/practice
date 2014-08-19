registrationModule.controller("CoursesController", function ($scope, courseRepository) {
    courseRepository.get().then(function (courses) { $scope.courses = courses; });
});