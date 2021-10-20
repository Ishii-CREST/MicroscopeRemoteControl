source APIFunctions.sh

IP=$1
PORT=$2
SAVE_DIR=$3

# Get newest retrieved images 

# Get Image Download URL
ImageDir=$(SendAPI GetNewestImageDir)
echo $ImageDir

# Download Images
wget -r -l1 -A tif -nc -nH --cut-dirs=1 -P $SAVE_DIR $ImageDir


