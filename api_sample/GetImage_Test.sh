source APIFunctions.sh

IP=$1
PORT=$2
BASEURL="http://$IP:$PORT/"

#---------------------
# Capture
SendAPI Capture

GET_DATE=$(date "+%Y%m%d_%H%M%S")
./GetImages.sh $IP $PORT $3/${GET_DATE}/


