<?php
/*
 * Raspberry Remote
 * http://xkonni.github.com/raspberry-remote/
 *
 * webinterface
 *
*/

$target = '192.168.2.110';
$port = 11337;

function daemon_send($target, $port, $output)
{
    $fp = fsockopen($target, $port, $errno, $errstr, 30) or die("$errstr ($errno)\n");
    fwrite($fp, $output);
    $state = "";
    while(!feof($fp))
    {
        $state .= fgets($fp, 2);
    }
    fclose($fp);
    return $state;
}
/*
 * get parameters
 */
if (isset($_GET['group'])) $nGroup=$_GET['group'];
else $nGroup="";
if (isset($_GET['switch'])) $nSwitch=$_GET['switch'];
else $nSwitch="";
if (isset($_GET['action'])) $nAction=$_GET['action'];
else $nAction="";
if (isset($_GET['delay'])) $nDelay=$_GET['delay'];
else $nDelay=0;
/*
 * actually send to the daemon
 * then reload the webpage without parameters
 * except for delay
 */
$output = $nGroup.$nSwitch.$nAction.$nDelay;
if (strlen($output) >= 8) {
    daemon_send($target, $port, $output);
    header("Location: index.php?delay=$nDelay");
    exit();
}
?>
<!DOCTYPE html>
<html lang="de" ng-app="pi443App">
<head>
    <meta charset="utf-8">
    <title>Pi443</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen">
    <link href="css/pi443.css" rel="stylesheet" media="screen">
    <script src="js/angular.min.js"></script>
</head>
<body ng-controller="pi443Ctrl">
<h1>{{data.name}}</h1>
<div class="spinner">
    <div class="ball ball-1"></div>
    <div class="ball ball-2"></div>
    <div class="ball ball-3"></div>
    <div class="ball ball-4"></div>
</div>

<div class="bs-docs-grid">
    <div class="row show-grid" ng-repeat="device in data.devices" ng-switch="device.type" ng-click="toggleState(device)">
        <a class="span1 pi443Item" ng-switch-when="fan" ng-switch="device.state" href="?group={{device.region}}&switch={{device.deviceId}}&action={{device.state}}delay={{device.delay}}">
            {{device.name}}
            <img class="pi443ItemImage fan" src="img/fan.svg" ng-switch-when="0" />
            <img class="pi443ItemImage fan rotate" src="img/fan.svg" ng-switch-when="1" />
        </a>
        <a class="span1 pi443Item"  ng-switch-when="lamp" ng-switch="device.state">
            {{device.name}}
            <img class="pi443ItemImage lamp" src="img/lamp.svg" ng-switch-when="0" />
            <img class="pi443ItemImage lamp" src="img/lamp_on.svg" ng-switch-when="1" />
        </a>
    </div>
</div>

<script src="http://code.jquery.com/jquery.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/pi443_controller.js"></script>
</body>
</html>