<?php
/*
 * Raspberry Remote
 * http://xkonni.github.com/raspberry-remote/
 *
 * configuration for the webinterface
 *
 */

/*
 * define ip address and port here
 */
$target = '192.168.2.114';
$port = 11337;

/*
 * specify configuration of sockets to use
 *   array("group", "plug", "description");
 * use empty string to create empty box
 *   ""
 *
 */
$config=array(
  array("11111", "01", "Fan"),
    array("11111", "03", "light left"),
    array("11111", "04", "light right"),
)
?>
