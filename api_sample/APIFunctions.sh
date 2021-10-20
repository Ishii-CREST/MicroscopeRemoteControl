
IP=$1
PORT=$2
BASEURL="http://$IP:$PORT/"

################
# Command split function
#[0]command split by [,]
function split()
 {
	# exec split
	IFS_ORIGINAL="$IFS"
	IFS=,
	local arr=($1)
	IFS="$IFS_ORIGINAL"
	
	echo "${arr[$2]}"
 }


##################
# API Send function.
#[0]:API
#[1]:args(args are combined with [,])
function SendAPI()
{
	local API=$1
	local Args=$2

	# split args
	IFS_ORIGINAL="$IFS"
	IFS=,
	local arr=($Args)
	IFS="$IFS_ORIGINAL"
		
	# loop each args
	for v in "${arr[@]}"; do
	  local curlArgs=$curlArgs" -d "$v
	done
    
	echo $(curl -f $curlArgs $BASEURL$API)
}

