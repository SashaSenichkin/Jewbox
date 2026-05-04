if [ -d "$DIR" ]; then

  cd Client
  echo "git pull project"
  git pull https://github.com/SashaSenichkin/Jewbox.git

else 

  git clone https://github.com/SashaSenichkin/Jewbox.git
  cd Client

fi

docker build ./Jewbox -t client
docker run -p 8080:8080  client