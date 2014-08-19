var registrationModule = angular.module("registrationModule", [])
.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/Registration/Courses', { templateUrl: '/templates/courses.html', controller: 'CoursesController' });
    $routeProvider.when('/Registration/Instructors', { templateUrl: '/templates/instructors.html', controller: 'InstructorsController' });
    $locationProvider.html5Mode(true);
});