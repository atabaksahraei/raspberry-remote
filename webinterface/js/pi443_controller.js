/**
 * Created by atabaksahraei on 22/08/2015.
 */

var pi443App = angular.module('pi443App', []);
pi443App.controller('pi443Ctrl',
    function($scope, $http) {
        console.info("controller");
        $http.get('data.json')
            .then(function (res) {
                $scope.data = res.data;
                console.debug($scope.data);
            });

        $scope.toggleState = function (device) {
            if (device.state == "0") {
                console.log("turn on");
                device.state = "1";
            } else {
                console.log("turn off");
                device.state = "0";
            }
            var location='?group='+device.region+'&switch='+device.deviceId+"&action="+device.state+"delay="+device.delay;
            //window.location = location;
            console.info(location);
        }
    });


//
//$(function(){
//console.log("ready!");
//$(".pi443ItemImage").click(function(){
//        console.log("clicked");
//        if($(this).hasClass("fan"))
//        {
//            console.log("fan");
//            if(!$(this).hasClass("rotate"))
//            {
//                $(this).addClass("rotate");
//                console.log("start rotate");
//            }else{
//                $(this).removeClass("rotate");
//                console.log("stop rotate");
//            }
//        }
//        if($(this).hasClass("lamp"))
//        {
//            console.log("lamp");
//            if(!$(this).hasClass("on"))
//            {
//                $(this).addClass("on");
//                $(this).attr("src", "img/lamp_on.svg");
//                console.log("start rotate");
//            }else{
//                $(this).removeClass("on");
//                $(this).attr("src", "img/lamp.svg");
//                console.log("stop rotate");
//            }
//        }
//    }
//);
//
//})