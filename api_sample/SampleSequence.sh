source APIFunctions.sh

IP=$1
PORT=$2
BASEURL="http://$IP:$PORT/"

###################
# Imaging EGFP with 920nm IR for 30 - 60 min.
##################
#//////////////////////////////
# Change IR Laser #1 wavelength to 920nm
SendAPI CameraSet_IRLasernWavelength 1,920

#or 
#curl -d 1 -d 920 $BASEURL"CameraSet_IRLasernWavelength"

#//////////////////////////////
# SetScannerWidth to 512
SendAPI CameraSet_ImagingScannerWidth 3

#or
#curl -d 3 $BASEURL"CameraSet_ImagingScannerWidth"

#//////////////////////////////
# Set ScanFrameRate to 1
SendAPI CameraSet_ScanFrameRateIndex 6

#or
#curl -d 6 $BASEURL"CameraSet_ScanFrameRateIndex"

#//////////////////////////////
# Reset ND Acquisition [Time] Tab settings
SendAPI ND_ResetTimeExp

#or
#curl $BASEURL"ND_ResetTimeExp"

#//////////////////////////////
# Reset ND Acquisition [XY] Tab settings
SendAPI ND_ResetMultipointExp

#or
#curl $BASEURL"ND_ResetMultipointExp"

#//////////////////////////////
# Reset ND Acquisition [Z] Tab settings
SendAPI ND_ResetZSeriesExp

#or
#curl $BASEURL"ND_ResetZSeriesExp"


#//////////////////////////////
# Set Z Setting
# Bottom -> Top
# Top = 10um
# Home = 0
# Bottom = 0um
# StepSize = 0um(auto calc)
# Step =3
# HomeDefined = 0
# CloseShutter = 1
SendAPI ND_SetZSeriesExp 0,10,0,0,0,3,0,1

#or
#curl -d 0 -d 10 -d 10 -d 0 -d 3 -d 0 -d 1 $BASEURL"ND_SetZSeriesExp"

#//////////////////////////////
# Append new row to ND Acquisition[Time] Tab
# 
SendAPI ND_AppendTimePhaseEx 10000,5,0,0

#or
#curl -d 10000 -d 5 -d 0 -d 0 $BASEURL"ND_AppendTimePhaseEx"

#//////////////////////////////
# Run ND Acquisiton
# Sync = 0 (Async)
# Time = 1(Enabled)
# XY = 0(Disabled)
# Z = 1(Enabled)
SendAPI ND_RunExperiment 0,1,0,1

#or
#curl -d 0 -d 1 -d 0 -d 1 $BASEURL"ND_RunExperiment"

#//////////////////////////////
# Get Retrieved Images
# Call script
GET_DATE=$(date "+%Y%m%d_%H%M%S")
./GetNDAcqImages.sh $IP $PORT $3/${GET_DATE}/


###################
#   After Imaging finished.
#   Stimulation -> Salvage cells.
##################
###################
# Move the cell to the center that must be focus.
##################
#//////////////////////////////
# Move XY Stage
# X = 100um (tentative
# Y = 110um (tentatie
SendAPI StgMoveXY 100,110

#or
#curl -d 100 -d 110 $BASEURL"StgMoveXY"

#//////////////////////////////
# Get current XY  stage
XYPos=$(SendAPI StgGetPosXY)

#or
#XYPos=curl $BASEURL"StgGetPosXY"

# disp XY position.
X=$(split $XYPos 0)
Y=$(split $XYPos 1)
echo "Xpos = ${X}"
echo "Ypos = ${Y}"


###################
# Imaging Cy3 and Cy5 in cell that is TICAtag+ by 720nm. 
##################
#//////////////////////////////
# Capture
SendAPI Capture
#or 
#curl $BASEURL"Capture"

GET_DATE=$(date "+%Y%m%d_%H%M%S")
./GetImages.sh $IP $PORT $3/${GET_DATE}/


###################
# Stimulation by 800nm(70mW 60sec)
##################

#//////////////////////////////
# Change IR Laser #1 wavelength to 800nm
SendAPI CameraSet_IRLasernWavelength 1 800

#or
#curl -d 1 -d 800 $BASEURL"CameraSet_IRLasernWavelength"

#//////////////////////////////
# Change Laser Power
# Ch = 1
# power = 60%
SendAPI CameraSet_CHnLaserPower 1,60

#or
#curl -d 1 -d 60 $BASEURL"CameraSet_CHnLaserPower"


#//////////////////////////////
# Clear ROIs
SendAPI ClearMeasROI

#or
#curl $BASEURL"ClearMeasROI"

#//////////////////////////////
# Create Rectangle ROI
# CenterX:128
# CenterY:128
# Width:80
# Height:80
# Angle:45
# Color:0
RoiID=$(SendAPI CreateRectangleROI 128,128,80,80,45,0)

#or
#curl -d 128 -d 128 -d 80 -d 80 -d 45 -d 0 $BASEURL"CreateRectangleROI"

#//////////////////////////////
# Change ROI Type (to Stimulation ROI)
# ROI ID:1 (CreateRectangleROI's return value)
# Type:3 (Stimulation)
SendAPI ChangeROIType $RoiID,3

#or
#curl -d $RoiID -d 3 $BASEURL"ChangeROIType"

#//////////////////////////////
# Reset ND Stimulation Phases
SendAPI ND_StimulationResetPhases

#or
#curl $BASEURL"ND_StimulationResetPhases"


#//////////////////////////////
# Append Phase to ND Stimulation
# Type = 1(Stim)
# Interval = 5.0(ms)
# Duratio1n = 10(ms)
SendAPI ND_StimulationAppendPhase 1,5.0,10

#or
#curl -d 1 -d 5.0 -d 10 $BASEURL"ND_StimulationAppendPhase"

#//////////////////////////////
# Append Phase to ND Stimulation
# Type = 0(Acq)
# Interval = 5.0(ms)
# Duration = 10(ms)
SendAPI ND_StimulationAppendPhase 0,5.0,10

#or
#curl -d 0 -d 5.0 -d 10 $BASEURL"ND_StimulationAppendPhase"


#//////////////////////////////
# Start ND Stimulation
SendAPI ND_RunSequentialStimulationExp 0

#or
#curl -d 0 $BASEURL"ND_RunSequentialStimulationExp"

#//////////////////////////////
# Get Retrieved Images
# Call script
GET_DATE=$(date "+%Y%m%d_%H%M%S")
./GetImages.sh $IP $PORT $3/${GET_DATE}/



