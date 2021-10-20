source APIFunctions.sh

IP=$1
PORT=$2
BASEURL="http://$IP:$PORT/"

#---------------------
# Run ND Stimulation"
# Sync = 0 (Async)
SendAPI ND_RunSequentialStimulationExp 0

#---------------------
# Get Retrieved Images
# Call script
GET_DATE=$(date "+%Y%m%d_%H%M%S")
./GetImages.sh $IP $PORT $3/${GET_DATE}/


