source APIFunctions.sh

IP=$1
PORT=$2
SAVE_DIR=$3
BASEURL="http://$IP:$PORT/"


# Get retrieved images while during ND Acquisition

while : 
do
		# Check is during ND Acquisition
        NDRunStatus=$(SendAPI ND_IsInExperimentCapture)
        echo "ND Run Status = "$NDRunStatus
	
        if [ "${NDRunStatus}" -eq 0 ] ; then
                echo "break"
                break
        fi
        		
		# Download Images
        ./GetImages.sh $IP $PORT $SAVE_DIR
done

# Just to make sure
./GetImages.sh $IP $PORT $SAVE_DIR

