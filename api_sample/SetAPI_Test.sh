source APIFunctions.sh

IP=$1
PORT=$2
BASEURL="http://$IP:$PORT/"

echo "<<<<< CameraSet_CHnChannelLaserIndex >>>>>"
SendAPI CameraSet_CHnChannelLaserIndex 1,4
SendAPI CameraSet_CHnChannelLaserIndex 2,3
SendAPI CameraSet_CHnChannelLaserIndex 3,2
SendAPI CameraSet_CHnChannelLaserIndex 4,1

echo "<<<<< CameraSet_CHnLaserPower >>>>>"
SendAPI CameraSet_CHnLaserPower 1,100
SendAPI CameraSet_CHnLaserPower 2,60
SendAPI CameraSet_CHnLaserPower 3,40
SendAPI CameraSet_CHnLaserPower 4,10

echo "<<<<< CameraSet_CHnLaserStimulationPower >>>>>"
SendAPI CameraSet_CHnLaserStimulationPower 1,1,10
SendAPI CameraSet_CHnLaserStimulationPower 2,1,20
SendAPI CameraSet_CHnLaserStimulationPower 3,1,30
SendAPI CameraSet_CHnLaserStimulationPower 4,1,40
SendAPI CameraSet_CHnLaserStimulationPower 1,2,50
SendAPI CameraSet_CHnLaserStimulationPower 2,2,60
SendAPI CameraSet_CHnLaserStimulationPower 3,2,70
SendAPI CameraSet_CHnLaserStimulationPower 4,2,80
SendAPI CameraSet_CHnLaserStimulationPower 1,3,90
SendAPI CameraSet_CHnLaserStimulationPower 2,3,100
SendAPI CameraSet_CHnLaserStimulationPower 3,3,90
SendAPI CameraSet_CHnLaserStimulationPower 4,3,80

echo "<<<<< CameraSet_ImagingScannerWidth >>>>>"
SendAPI CameraSet_ImagingScannerWidth 4

echo "<<<<< CameraSet_ImagingZoom >>>>>"
SendAPI CameraSet_ImagingZoom 10

echo "<<<<< CameraSet_IRLasernWavelength >>>>>"
SendAPI CameraSet_IRLasernWavelength 1,980
SendAPI CameraSet_IRLasernWavelength 2,720

echo "<<<<< CameraSet_LineAcquireMode >>>>>"
SendAPI CameraSet_LineAcquireMode 1

echo "<<<<< CameraSet_LineAveraging >>>>>"
SendAPI CameraSet_LineAveraging 4

echo "<<<<< CameraSet_ScanFrameRateIndex >>>>>"
SendAPI CameraSet_ScanFrameRateIndex 5

echo "<<<<< CameraSet_StimulationTime >>>>>"
SendAPI CameraSet_StimulationTime 1,12.34
SendAPI CameraSet_StimulationTime 2,56.78
SendAPI CameraSet_StimulationTime 3,91.09

echo "<<<<< CreateRectangleROI >>>>>"
SendAPI Capture
RoiID=$(SendAPI CreateRectangleROI 128,128,80,80,45,0)

echo "<<<<< ChangeROIType >>>>>"
SendAPI ChangeROIType $RoiID,2

echo "<<<<< ND_AppendMultipointPoint >>>>>"
SendAPI ND_AppendMultipointPoint 12.34,56.78,9.10
SendAPI ND_AppendMultipointPoint 10.98,76.54,32.1,TestPointName

echo "<<<<< ND_AppendTimePhaseEx >>>>>"
SendAPI ND_AppendTimePhaseEx 10,9
SendAPI ND_AppendTimePhaseEx 8,7,TestCommand
SendAPI ND_AppendTimePhaseEx 6,5,TestCommand2,TestPhase

echo "<<<<< ND_SetZSeriesExp >>>>>"
SendAPI ND_SetZSeriesExp 0,1000,500,800,100,10,0,1
SendAPI ND_SetZSeriesExp 0,1000,500,800,100,10,0,1,"TiZDrive"
SendAPI ND_SetZSeriesExp 0,1000,500,800,100,10,0,1,'"''"',"TestBefore"
SendAPI ND_SetZSeriesExp 0,1000,500,800,100,10,0,1,'"''"',"TestBefore","TestAfter"


echo "<<<<< ND_StimulationAppendPhase >>>>>"
SendAPI ND_StimulationAppendPhase 1,123.456,789.10

echo "<<<<< StgGetPosZ >>>>>"
SendAPI StgMoveXY 1234.567,9876.543

echo "<<<<< ND_ResetMultipointExp >>>>>"
SendAPI ND_ResetMultipointExp

echo "<<<<< ND_ResetTimeExp >>>>>"
SendAPI ND_ResetTimeExp

echo "<<<<< ND_ResetZSeriesExp >>>>>"
SendAPI ND_ResetZSeriesExp

echo "<<<<< ND_StimulationResetPhases >>>>>"
SendAPI ND_StimulationResetPhases


