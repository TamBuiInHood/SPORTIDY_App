import { Ionicons } from '@expo/vector-icons';
import React from 'react';
import { StyleSheet, Text, View } from 'react-native';

const ProgressBar = ({ currentStep }: { currentStep: number }) => {
  return (
    <View style={styles.progressContainer}>
      {/* Step 1 */}
      <View style={styles.stepContainer}>
        <View style={styles.iconWrapper}>
          {currentStep >= 1 ? (
            <Ionicons name="checkmark-circle-outline" size={24} color="#f39c12" />
          ) : (
            <Ionicons name="radio-button-off-outline" size={24} color="#ddd" />
          )}
        </View>
        <Text style={styles.stepLabel}>You choose</Text>
      </View>

      {/* Divider */}
      <View style={[styles.divider, currentStep >= 2 ? styles.activeDivider : null]} />

      {/* Step 2 */}
      <View style={styles.stepContainer}>
        <View style={styles.iconWrapper}>
          {currentStep >= 2 ? (
            <Ionicons name="accessibility" size={24} color="#f39c12" />
          ) : (
            <Ionicons name="radio-button-off-outline" size={24} color="#ddd" />
          )}
        </View>
        <Text style={styles.stepLabel}>Booking</Text>
      </View>

      {/* Divider */}
      <View style={[styles.divider, currentStep >= 3 ? styles.activeDivider : null]} />

      {/* Step 3 */}
      <View style={styles.stepContainer}>
        <View style={styles.iconWrapper}>
          {currentStep >= 3 ? (
            <Ionicons name="checkmark-circle-outline" size={24} color="#f39c12" />
          ) : (
            <Ionicons name="radio-button-off-outline" size={24} color="#ddd" />
          )}
        </View>
        <Text style={styles.stepLabel}>Success</Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  progressContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginTop: 30
  },
  stepContainer: {
    flexDirection: 'column',
    alignItems: 'center',
    flex: 1,
  },
  iconWrapper: {
    padding: 5,
  },
  stepLabel: {
    fontSize: 12,
    color: '#333',
  },
  divider: {
    flex: 1,
    height: 1,
    backgroundColor: '#ddd',
    marginHorizontal: 5,
  },
  activeDivider: {
    backgroundColor: '#f39c12',
  },
});

export default ProgressBar;
