import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { RootStackParamList   
 } from "../../types/types";
import { useNavigation } from '@react-navigation/native';


type YourClubNavigationProp = NativeStackNavigationProp<
  RootStackParamList,
  "YourClub"
>;

interface YourClubProps {
  clubName: string;
  playerCount: number;
  mainSport: string;
  secondarySport: string;
  location: string;
  district: string;
}

const YourClub: React.FC<YourClubProps> = ({
  clubName,
  playerCount,
  mainSport,
  secondarySport,
  location,
  district, 
}) => {
  const navigation = useNavigation<YourClubNavigationProp>(); 

  return (
    <View style={styles.container}>
      <View style={styles.headerContainer}>
        <Image
          source={{ uri: 'https://cdn.builder.io/api/v1/image/assets/TEMP/c9bc2ebbfbf6c271ddf7f9cd2b9803e9215563a1aac637d63788c5791fbcdcb3?placeholderIfAbsent=true&apiKey=f8ccf21cf8ce4053b555f169e17dcf6a' }}
          style={styles.clubLogo}
        />
        <View style={styles.clubNameContainer}>
          <Text style={styles.clubName}>{clubName}</Text> 
        </View>
      </View>
      <View style={styles.playerCountContainer}>
        <Text style={styles.playerCount}>{playerCount} players</Text> 
      </View>
      <View style={styles.sportsContainer}>
        <Image
          source={{ uri: 'https://cdn.builder.io/api/v1/image/assets/TEMP/0b304679551c8821cbf6de107bd09766f625a9ec386f9312317a191165e586b8?placeholderIfAbsent=true&apiKey=f8ccf21cf8ce4053b555f169e17dcf6a' }}
          style={styles.sportsIcon}
        />
        <View style={styles.sportsTextContainer}>
          <Text style={styles.sportsLabel}>Main Sport:</Text>
          <Text style={styles.sportName}>{mainSport}</Text> 
          <View style={styles.sportsSeparator} />
          <View style={styles.secondarySportContainer}>
            <Text style={styles.sportName}>{secondarySport}</Text> 
          </View>
        </View>
      </View>
      <View style={styles.locationContainer}>
        <Image
          source={{ uri: 'https://cdn.builder.io/api/v1/image/assets/TEMP/df71b1ab89e658be9c9d0ce31b3431a144ed1135d610f63c78128f05ae8c17d7?placeholderIfAbsent=true&apiKey=f8ccf21cf8ce4053b555f169e17dcf6a' }}
          style={styles.locationIcon}
        />
        <Text style={styles.locationLabel}>Location:</Text>
        <View style={styles.locationTextContainer}>
          <Text style={styles.locationName}>{location}</Text> 
          <View style={styles.locationSeparator} />
          <View style={styles.districtContainer}>
            <Text style={styles.districtName}>{district}</Text> 
          </View>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: 'column',
    paddingHorizontal: 24,
    paddingBottom: 10,
    marginTop: 48,
    width: '100%',
    backgroundColor: 'white',
    borderRadius: 16,
    borderWidth: 3,
    borderColor: '#d4d4d8',
    maxWidth: 382,
  },
  headerContainer: {
    flexDirection: 'row',
    zIndex: 10,
    gap: 10,
    alignItems: 'flex-start',
    alignSelf: 'flex-start',
    marginTop: 0,
  },
  clubLogo: {
    width: 50,
    height: 50,
    resizeMode: 'contain',
    alignSelf: 'flex-start',
  },
  clubNameContainer: {
    alignSelf: 'flex-end',
    marginTop: 20,
    flexBasis: 'auto',
  },
  clubName: {
    fontSize: 20,
    fontWeight: '600',
    color: 'black',
  },
  playerCountContainer: {
    flexDirection: 'row',
    gap: 20,
    alignSelf: 'flex-start',
    marginTop: 6,
    marginLeft: 64,
  },
  playerCount: {
    fontSize: 16,
    fontWeight: '600',
    color: 'black',
  },
  sportsContainer: {
    flexDirection: 'row',
    gap: 16,
    alignSelf: 'flex-end',
  },
  sportsIcon: {
    width: 25,
    height: 25,
    resizeMode: 'contain',
  },
  sportsTextContainer: {
    flexDirection: 'row',
    gap: 6,
    alignItems: 'center',
  },
  sportsLabel: {
    fontSize: 14,
    fontWeight: '500',
    color: 'black',
  },
  sportName: {
    fontSize: 14,
    fontWeight: '500',
    color: 'black',
  },
  sportsSeparator: {
    width: 1,
    height: 20,
    borderWidth: 1,
    borderColor: '#d4d4d8',
  },
  secondarySportContainer: {
    gap: 10,
    alignSelf: 'stretch',
    padding: 2,
    backgroundColor: 'white',
    borderRadius: 6,
  },
  locationContainer: {
    flexDirection: 'row',
    gap: 20,
    justifyContent: 'space-between',
    alignItems: 'flex-start',
    alignSelf: 'flex-end',
    maxWidth: '100%',
    width: 266,
  },
  locationIcon: {
    width: 16,
    height: 20,
    resizeMode: 'contain',
  },
  locationLabel: {
    alignSelf: 'stretch',
    fontSize: 14,
    fontWeight: '500',
    color: 'black',
  },
  locationTextContainer: {
    flexDirection: 'row',
    gap: 6,
  },
  locationName: {
    fontSize: 14,
    fontWeight: '500',
    color: 'black',
  },
  locationSeparator: {
    width: 1,
    height: 20,
    borderWidth: 1,
    borderColor: '#d4d4d8',
  },
  districtContainer: {
    gap: 10,
    alignSelf: 'stretch',
    padding: 2,
    backgroundColor: 'white',
    borderRadius: 6,
  },
  districtName: {
    fontSize: 14,
    fontWeight: '500',
    color: 'black',
  },
});

export default YourClub;