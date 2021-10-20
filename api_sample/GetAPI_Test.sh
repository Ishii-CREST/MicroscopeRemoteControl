source APIFunctions.sh

IP=$1
PORT=$2
BASEURL="http://$IP:$PORT/"

echo "<<<<< CameraGet_CHnLaserStimulationPower >>>>>"
SendAPI CameraGet_CHnLaserStimulationPower 1,1
SendAPI CameraGet_CHnLaserStimulationPower 1,2
SendAPI CameraGet_CHnLaserStimulationPower 1,3
SendAPI CameraGet_CHnLaserStimulationPower 2,1
SendAPI CameraGet_CHnLaserStimulationPower 2,2
SendAPI CameraGet_CHnLaserStimulationPower 2,3
SendAPI CameraGet_CHnLaserStimulationPower 3,1
SendAPI CameraGet_CHnLaserStimulationPower 3,2
SendAPI CameraGet_CHnLaserStimulationPower 3,3
SendAPI CameraGet_CHnLaserStimulationPower 4,1
SendAPI CameraGet_CHnLaserStimulationPower 4,2
SendAPI CameraGet_CHnLaserStimulationPower 4,3

echo "<<<<< CameraGet_ImagingScannerWidth >>>>>"
SendAPI CameraGet_ImagingScannerWidth

echo "<<<<< CameraGet_ImagingZoom >>>>>"
SendAPI CameraGet_ImagingZoom

echo "<<<<< CameraGet_IRLasernWavelength >>>>>"
SendAPI CameraGet_IRLasernWavelength 1
SendAPI CameraGet_IRLasernWavelength 2

echo "<<<<< CameraGet_LineAveraging >>>>>"
SendAPI CameraGet_LineAveraging

echo "<<<<< CameraGet_ScanFrameRateIndex >>>>>"
SendAPI CameraGet_ScanFrameRateIndex

echo "<<<<< CameraGet_StimulationTime >>>>>"
SendAPI CameraGet_StimulationTime 1

echo "<<<<< GetCurrentNDExpImageCount >>>>>"
SendAPI GetCurrentNDExpImageCount

echo "<<<<< GetCurrentNDExpXYZ >>>>>"
SendAPI GetCurrentNDExpXYZ

echo "<<<<< GetNDExperimentImageCount >>>>>"
SendAPI GetNDExperimentImageCount

echo "<<<<< GetPixelSize >>>>>"
SendAPI GetPixelSize

echo "<<<<< GetROIInfo >>>>>"
SendAPI Capture
RoiID=$(SendAPI CreateRectangleROI 128,128,80,80,45,0)
SendAPI GetROIInfo $RoiID

echo "<<<<< ND_IsInExperimentCapture >>>>>"
SendAPI ND_IsInExperimentCapture

echo "<<<<< ND_MP_GetCount >>>>>"
SendAPI ND_MP_GetCount

echo "<<<<< RangeOfND_GetZSeriesExp >>>>>"
SendAPI RangeOfND_GetZSeriesExp

echo "<<<<< StepOfND_GetZSeriesExp >>>>>"
SendAPI StepOfND_GetZSeriesExp

echo "<<<<< StgGetPosXY >>>>>"
SendAPI StgGetPosXY

echo "<<<<< StgGetPosZ >>>>>"
SendAPI StgGetPosZ

echo "<<<<< GetImagingInfo >>>>>"
SendAPI GetImagingInfo
