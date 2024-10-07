import React, { useState } from "react";
import { Text, TouchableOpacity, View, StyleSheet } from "react-native";
import { useNavigation } from '@react-navigation/native'; // Import the useNavigation hook
import { RootStackParamList } from "@/types/types";
import { NativeStackNavigationProp } from "react-native-screens/lib/typescript/native-stack/types";

type YourClubNavigationProp = NativeStackNavigationProp<
  RootStackParamList,
  "YourClub"
>;

const ActionButtonClub: React.FC = () => {
  const [activeTab, setActiveTab] = useState<'available' | 'your club'>('available'); // Default tab is 'available'
  const navigation = useNavigation<YourClubNavigationProp>();

  const handleMeetButtonPress = () => {
    setActiveTab('your club');
    navigation.navigate('YourClub', { 
      clubName: "Sportidy Night Club", 
      playerCount: 12, 
      mainSport: "Football", 
      secondarySport: "Badminton", 
      location: "Thu Duc", 
      district: "District 9" 
    }); 
  };

  return (
    <View>
      {/* Buttons to switch tabs */}
      <View style={styles.actionButtons}>
        <TouchableOpacity 
          style={[styles.availableButton, activeTab === 'available' && styles.activeTabButton]} // Apply active style
          onPress={() => console.log('Available pressed')} 
        >
          <Text style={[styles.availableButtonText, activeTab === 'available' && styles.activeTabText]}>
            Available
          </Text>
        </TouchableOpacity>
        <TouchableOpacity 
          style={[styles.meetButton, activeTab === 'your club' && styles.activeTabButton]} // Apply active style
          onPress={() => {
            handleMeetButtonPress(); // Navigate to YourMeetScreen
          }}
        >
          <Text style={[styles.meetButtonText, activeTab === 'your club' && styles.activeTabText]}>
            Your Meet
          </Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  actionButtons: {
    position: 'absolute',
    top: 80, 
    flexDirection: 'row',
    justifyContent: 'space-around',
    width: '100%',
    paddingHorizontal: 20,
  },
  availableButton: {
    backgroundColor: '#FF915D',
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 50,
    alignItems: 'center',
  },
  availableButtonText: {
    color: '#fff',
    fontWeight: 'bold',
  },
  meetButton: {
    backgroundColor: '#fff',
    borderWidth: 1,
    borderColor: '#FF915D',
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 50,
    alignItems: 'center',
  },
  meetButtonText: {
    color: '#FF915D',
    fontWeight: 'bold',
  },
  content: {
    marginTop: 150,
    alignItems: 'center',
  },
  activeTabButton: {
    backgroundColor: '#FF915D',
    borderColor: '#FF915D',
  },
  activeTabText: {
    color: '#fff',
  },
});

export default ActionButtonClub;