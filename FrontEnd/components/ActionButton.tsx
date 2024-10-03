import React, { useState } from "react";
import { Text, TouchableOpacity, View, StyleSheet } from "react-native";

const ActionButtons: React.FC = () => {
  const [activeTab, setActiveTab] = useState<'available' | 'meet'>('available');

  return (
    <View>
      {/* Buttons to switch tabs */}
      <View style={styles.actionButtons}>
        <TouchableOpacity 
          style={[
            styles.availableButton, 
            activeTab === 'available' && styles.activeTabButton
          ]}
          onPress={() => setActiveTab('available')}
        >
          <Text 
            style={[
              styles.availableButtonText, 
              activeTab === 'available' && styles.activeTabText
            ]}
          >
            Available
          </Text>
        </TouchableOpacity>
        <TouchableOpacity 
          style={[
            styles.meetButton, 
            activeTab === 'meet' && styles.activeTabButton
          ]}
          onPress={() => setActiveTab('meet')}
        >
          <Text 
            style={[
              styles.meetButtonText, 
              activeTab === 'meet' && styles.activeTabText
            ]}
          >
            Your Meet
          </Text>
        </TouchableOpacity>
      </View>

      <View style={styles.content}>
        {activeTab === 'available' ? (
          <Text>Showing Available Content</Text>
        ) : (
          <Text>Showing Your Meet Content</Text>
        )}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  actionButtons: {
    position: 'absolute',
    top: -220, 
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
  // Styles for active tab
  activeTabButton: {
    backgroundColor: '#FF915D',
    borderColor: '#FF915D',
  },
  activeTabText: {
    color: '#fff',
  },
});

export default ActionButtons;
