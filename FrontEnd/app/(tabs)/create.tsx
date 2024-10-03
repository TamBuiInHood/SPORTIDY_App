import React, { useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, TextInput, Button, ScrollView, Alert } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import SportChoose from '@/components/SportChoose';
import axios from 'axios';
import DateTimePickerModal from 'react-native-modal-datetime-picker';
import { format } from 'date-fns';

const CreateMeetScreen = () => {
  const [club, setClub] = useState(null);
  const [sport, setSport] = useState<number | null>(null);
  const [date, setDate] = useState(new Date());
  const [time, setTime] = useState(new Date());
  const [duration, setDuration] = useState(1);
  const [location, setLocation] = useState('');
  const [cancelTime, setCancelTime] = useState(4);
  const [numPlayers, setNumPlayers] = useState(12);
  const [meetName, setMeetName] = useState('');
  const [notes, setNotes] = useState('');
  const [isDatePickerVisible, setDatePickerVisibility] = useState(false);
  const [isTimePickerVisible, setTimePickerVisibility] = useState(false);

  const showDatePicker = () => setDatePickerVisibility(true);
  const hideDatePicker = () => setDatePickerVisibility(false);
  const handleConfirmDate = (selectedDate: any) => {
    setDate(selectedDate);
    hideDatePicker();
  };

  const showTimePicker = () => setTimePickerVisibility(true);
  const hideTimePicker = () => setTimePickerVisibility(false);
  const handleConfirmTime = (selectedTime: any) => {
    setTime(selectedTime);
    hideTimePicker();
  };

  const handleCreateMeet = async () => {
    if (!meetName || !sport || !location) {
      Alert.alert("Missing fields", "Please fill in all required fields.");
      return;
    }
  
    // Formatting the startDate and endDate properly
    const formattedStartDate = format(date, 'dd-MM-yyyy HH:mm'); // Format date using date-fns
    const endDateTime = new Date(date.getTime() + duration * 60 * 60 * 1000); // Add duration hours to the start date
    const formattedEndDate = format(endDateTime, 'dd-MM-yyyy HH:mm'); // Format end date
  
    const formData = new FormData();
    formData.append('meetingImage', ''); // Add image if available
    formData.append('meetingName', meetName || "Test Meeting");
    formData.append('endDate', formattedEndDate);
    formData.append('sportId', sport.toString()); // Ensure this is a string
    formData.append('startDate', formattedStartDate);
    formData.append('address', location || "Location");
    formData.append('cancelBefore', cancelTime.toString()); // Ensure this is a string
    formData.append('totalMember', numPlayers.toString()); // Ensure this is a string
    formData.append('currentIdLogin', '2'); // Assuming this is the logged-in user
    formData.append('isPublic', true);
    formData.append('clubId', '');
    formData.append('note', '');
    console.log("FormData: ", formData);
  
    try {
      const response = await axios.post(
        'https://fsusportidyapi20241001230520.azurewebsites.net/sportidy/meetings',
        formData,
        {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        }
      );
      Alert.alert("Success", "Meeting created successfully!");
      console.log(response.data);
    } catch (error: any) {
      console.error("Request error:", error.response?.data || error.message);
  console.error("Response Status:", error.response?.status);
  console.error("Response Headers:", error.response?.headers);
  Alert.alert("Error", "Failed to create meeting. Please try again.");
    }
  };
  
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.backButton} onPress={() => {}}>
          <Ionicons name="arrow-back-circle-outline" color={"#ffff"} size={30} />
        </TouchableOpacity>
        <Text style={styles.headerText}>CREATE A MEET</Text>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContainer}>
        <View style={styles.sportOptions}>
          <SportChoose setSport={setSport} />
        </View>

        <Text style={styles.sectionTitle}>MEET</Text>
        <View style={styles.form}>
          <View style={styles.formItem}>
            <View style={styles.formLabelContainer}>
              <Ionicons name="calendar-outline" color={"#ff951d"} size={20} />
              <Text style={styles.formLabel}>Select date</Text>
            </View>
            <TouchableOpacity onPress={showDatePicker} style={styles.dateButton}>
              <Text style={styles.dateText}>{date.toDateString()}</Text>
            </TouchableOpacity>
            <DateTimePickerModal
              isVisible={isDatePickerVisible}
              mode="date"
              onConfirm={handleConfirmDate}
              onCancel={hideDatePicker}
            />
          </View>

          <View style={styles.formItem}>
            <View style={styles.formLabelContainer}>
              <Ionicons name="timer-outline" color={"#ff951d"} size={20} />
              <Text style={styles.formLabel}>Select time</Text>
            </View>
            <TouchableOpacity onPress={showTimePicker} style={styles.dateButton}>
              <Text>{time.toLocaleTimeString()}</Text>
            </TouchableOpacity>
            <DateTimePickerModal
              isVisible={isTimePickerVisible}
              mode="time"
              onConfirm={handleConfirmTime}
              onCancel={hideTimePicker}
            />
          </View>

          <View style={styles.formItem}>
            <View style={styles.formLabelContainer}>
              <Ionicons name="time-outline" color={"#ff951d"} size={20} />
              <Text style={styles.formLabel}>Duration</Text>
            </View>
            <TextInput
              style={styles.formInput}
              value={duration.toString()}
              onChangeText={(text) => setDuration(parseInt(text) || 1)}
              keyboardType="numeric"
            />
            <Text style={styles.formUnit}>hour(s)</Text>
          </View>

          <View style={styles.formItemMeeting}>
            <View style={styles.formLabelContainer}>
              <Ionicons name="location-outline" color={"#ff951d"} size={20} />
              <Text style={styles.formLabel}>Set location</Text>
            </View>
            <TextInput
              style={styles.formInputMeeting}
              value={location}
              onChangeText={setLocation}
              placeholder="Enter location"
            />
          </View>

          <View style={styles.formItem}>
            <Text style={styles.formLabel}>Set time to cancel</Text>
            <TextInput
              style={styles.formInput}
              value={cancelTime.toString()}
              onChangeText={(text) => setCancelTime(parseInt(text) || 4)}
              keyboardType="numeric"
            />
            <Text style={styles.formUnit}>hour(s)</Text>
          </View>

          <View style={styles.formItem}>
            <View style={styles.formLabelContainer}>
              <Ionicons name="person-add-outline" color={"#ff951d"} size={20} />
              <Text style={styles.formLabel}>Number of players</Text>
            </View>
            <View style={styles.playerCounter}>
              <TouchableOpacity onPress={() => setNumPlayers(Math.max(numPlayers - 1, 1))} style={styles.counterButton}>
                <Text style={styles.counterButtonText}>-</Text>
              </TouchableOpacity>
              <Text style={styles.counterText}>{numPlayers}</Text>
              <TouchableOpacity onPress={() => setNumPlayers(numPlayers + 1)} style={styles.counterButton}>
                <Text style={styles.counterButtonText}>+</Text>
              </TouchableOpacity>
            </View>
          </View>

          <View style={styles.formItemMeeting}>
            <Text style={styles.formLabelMeeting}>Meet Name</Text>
            <TextInput
              style={styles.formInputMeeting}
              value={meetName}
              onChangeText={setMeetName}
              placeholder="Enter meet name"
            />
            <TextInput
              style={styles.formInputMeeting}
              value={notes}
              onChangeText={setNotes}
              multiline={true}
              placeholder="Add notes"
            />
          </View>
        </View>

        <View style={styles.buttonContainer}>
          <Button title="Create Meet" onPress={handleCreateMeet} color="#f9ca24" />
        </View>
      </ScrollView>
    </View>
  );
};
const styles = StyleSheet.create({

  container: {
    flex: 1,
    backgroundColor: '#fff',
    marginTop: 50
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#f9ca24',
    padding: 16,
    position: 'relative',
    justifyContent: 'center'
  },
  backButton: {
    padding: 8,
    position: 'absolute',
    left: 10
  },
  headerText: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#fff',
    marginLeft: 16,

  },
  addClubSection: {
    padding: 16,
    flexDirection: 'row',
    justifyContent: 'space-evenly',
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
    paddingBottom: 10,
  },
  title: {
    fontSize: 16,
    fontWeight: 'bold'
  },
  addClubText: {
    color: '#2C4BBB',
    fontSize: 16,
    textDecorationLine: 'underline',
    fontWeight: 'bold'
  },
  clubSection: {
    padding: 16,
    backgroundColor: '#f9ca24',
    borderRadius: 8,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  clubName: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: 18,
  },
  clubSchedule: {
    color: '#fff',
    fontSize: 14,
  },
  removeClubButton: {
    padding: 8,
  },
  removeClubText: {
    color: '#fff',
    textDecorationLine: 'underline',
  },

  sportChoices: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    width: '100%',

  },
  inputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  sportOptions: {
    paddingBottom: 10,
  },
  sportOption: {
    flexDirection: 'column',
    alignItems: 'center',
    padding: 10,
    borderWidth: 1,
    borderColor: '#ff951d',
    borderRadius: 20,
    width: 120,
    height: 80,
  },
  sportOptionSelected: {
    backgroundColor: '#ff951d',
    color: '#ffff'
  },
  sportOptionText: {
    marginTop: 5,
    fontSize: 14,
    color: '#000',
  },
  sportOptionTextSelected: {
    color: '#fff',
    fontWeight: 'bold'
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    padding: 16,
    color: '#ff951d'
  },
  form: {
    paddingHorizontal: 15,
  },
  formItem: {
    marginBottom: 10,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',

  },
  formLabel: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#333',
  },
  formLabelContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  formIcon: {
    marginRight: 10,
  },
  formInput: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 50,
    padding: 10,
    width: 40,
    height: 40,
    justifyContent: 'center',
    alignItems: 'center',
  },
  formUnit: {
    marginTop: 8,
    color: '#888',
  },
  dateButton: {
    backgroundColor: '#f1f1f1',
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 5,
    justifyContent: 'center',
    alignItems: 'center',
  },
  dateText: {
    fontSize: 16,
    color: '#333',
  },
  checkbox: {
    borderWidth: 1,
    borderColor: '#f9ca24',
    width: 24,
    height: 24,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 8,

  },
  checkboxChecked: {
    width: 16,
    height: 16,
    backgroundColor: '#f9ca24',

  },
  checkboxUnchecked: {
    width: 16,
    height: 16,
    backgroundColor: '#fff',
  },
  playerCounter: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 8,
  },
  counterButton: {
    width: 40,
    height: 40,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#f9ca24',
    borderRadius: 20,
  },
  counterButtonText: {
    fontSize: 20,
    color: '#fff',
  },
  counterText: {
    marginHorizontal: 16,
    fontSize: 16,
  },
  buttonContainer: {
    padding: 16,
    marginBottom: 60
  },
  formInputMeeting: {
    width: '100%', // Make the input take the full width of the screen
    padding: 10,
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    fontSize: 16,
    marginVertical: 10

  },
  formItemMeeting: {
    marginBottom: 16,
    flexDirection: 'column',
  },
  formLabelMeeting: {
    fontSize: 16,
    fontWeight: 'bold',
    marginBottom: 8,
  },

});



export default CreateMeetScreen;
