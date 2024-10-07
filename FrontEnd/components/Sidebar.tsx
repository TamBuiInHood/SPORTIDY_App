// Sidebar.tsx
import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';

interface SidebarProps {
  isVisible: boolean; 
  onClose: () => void; 
}

const Sidebar: React.FC<SidebarProps> = ({ isVisible, onClose }) => {
  if (!isVisible) return null; 

  return (
    <View style={styles.sidebar}>
      <TouchableOpacity onPress={onClose} style={styles.closeButton}>
        <Text style={styles.closeText}>Close</Text>
      </TouchableOpacity>
      <TouchableOpacity>
        <Text style={styles.menuItem}>View history booking fields</Text>
      </TouchableOpacity>
      <TouchableOpacity>
        <Text style={styles.menuItem}>Feedback</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  sidebar: {
    position: 'absolute',
    top: 10,
    left: 0,
    height: '100%',
    width: '80%',
    backgroundColor: '#fff',
    shadowColor: '#000',
    shadowOpacity: 0.5,
    shadowRadius: 5,
    elevation: 5,
    padding: 20,
    zIndex: 10,
  },
  closeButton: {
    marginBottom: 10,
  },
  closeText: {
    fontSize: 18,
    fontWeight: 'bold',
  },
  menuItem: {
    fontSize: 16,
    marginVertical: 5,
  },
});

export default Sidebar;
