source APIFunctions.sh

IP=$1
PORT=$2
BASEURL="http://$IP:$PORT/"

#---------------------
# Run ND Acquisiton
# Sync = 0 (Async)
# Time = 1(Enabled)
# XY = 1(Enabled)
# Z = 1(Enabled)
SendAPI ND_RunExperiment 0,1,0,1

#---------------------
# Get Retrieved Images
# Call script
GET_DATE=$(date "+%Y%m%d_%H%M%S")
./GetNDAcqImages.sh $IP $PORT $3/${GET_DATE}/
