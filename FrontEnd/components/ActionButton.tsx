import React, { useState } from "react";
import { Text, TouchableOpacity, View, StyleSheet } from "react-native";
import { useNavigation } from '@react-navigation/native'; // Import the useNavigation hook
import { RootStackParamList } from "@/types/types";
import { NativeStackNavigationProp } from "react-native-screens/lib/typescript/native-stack/types";

type HomeScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, "HomeScreen">;

const ActionButtons: React.FC = () => {
  const [activeTab, setActiveTab] = useState<'available' | 'meet'>('available'); // Default tab is 'available'
  const navigation = useNavigation<HomeScreenNavigationProp>(); // Ensure the correct type for navigation

  const handleMeetButtonPress = () => {
    setActiveTab('meet');
    navigation.navigate('YourMeeting'); // Navigate to YourMeeting screen
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
          style={[styles.meetButton, activeTab === 'meet' && styles.activeTabButton]} // Apply active style
          onPress={() => {
            handleMeetButtonPress(); // Navigate to YourMeetScreen
          }}
        >
          <Text style={[styles.meetButtonText, activeTab === 'meet' && styles.activeTabText]}>
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

export default ActionButtons;
