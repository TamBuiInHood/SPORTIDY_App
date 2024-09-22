import { Ionicons } from "@expo/vector-icons";
import React from "react";
import { Text, TouchableOpacity, View, StyleSheet } from "react-native";

const ActionButtons: React.FC = () => {
    return (
      <View style={styles.actionButtons}>
        <TouchableOpacity style={styles.availableButton}>
          <Text style={styles.availableButtonText}>Available</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.meetButton}>
          <Text style={styles.meetButtonText}>Your meet</Text>
        </TouchableOpacity>
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
});

export default ActionButtons;
