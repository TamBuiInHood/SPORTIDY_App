import React, { useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, TextInput, Button, ScrollView, Alert } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import SportChoose from '@/components/SportChoose';
import DateTimePickerModal from 'react-native-modal-datetime-picker';
import { format } from 'date-fns';
import api from '@/config/api';

const CreateMeetScreen = () => {
  const [sport, setSport] = useState<number | null>(null);
  const [isDatePickerVisible, setDatePickerVisibility] = useState(false);
  const [isSelectingStartDate, setIsSelectingStartDate] = useState(true);

  const showDateTimePicker = (isStartDate: boolean) => {
    setIsSelectingStartDate(isStartDate);
    setDatePickerVisibility(true);
  };


  const hideDateTimePicker = () => {
    setDatePickerVisibility(false);
  };

  const handleConfirmDate = (selectedDate: Date) => {
    const formattedDate = format(selectedDate, "yyyy-MM-dd HH:mm:ss");
  
    if (isSelectingStartDate) {
      setMeeting((prevMeeting) => ({
        ...prevMeeting,
        data: { ...prevMeeting.data, startDate: formattedDate }
      }));
    } else {
      setMeeting((prevMeeting) => ({
        ...prevMeeting,
        data: { ...prevMeeting.data, endDate: formattedDate }
      }));
    }
  
    hideDateTimePicker();
  };
  const [meeting, setMeeting] = useState({
    data: {
      address: "Gcctc",
      cancelBefore: 1,
      currentLogin: 2,
      endDate: "2024-10-06T22:46:18",
      isPublic: true,
      meetingImage: "",
      meetingName: "Ctcyf",
      note: " G gv g",
      sportId: 1,
      startDate: "2024-10-07T21:46:18",
      totalMember: 1
    }
  });
  const handleInputChange = (name: string, value: any) => {
    setMeeting((prevMeeting) => ({
      ...prevMeeting,
      data: { ...prevMeeting.data, [name]: value }
    }));
  };
  const decrementMember = () => {
    setMeeting((prev) => ({
      ...prev,
      data: {
        ...prev.data,
        totalMember: Math.max(prev.data.totalMember - 1, 1),
      },
    }));
  };
  
  const incrementMember = () => {
    setMeeting((prev) => ({
      ...prev,
      data: {
        ...prev.data,
        totalMember: prev.data.totalMember + 1,
      },
    }));
  };

  const handleCreateMeeting = async () => {
    try {
        const payload = {
            address: meeting.data.address,
            cancelBefore: meeting.data.cancelBefore,
            currentLogin: meeting.data.currentLogin,
            endDate: meeting.data.endDate,
            isPublic: meeting.data.isPublic,
            meetingImage: meeting.data.meetingImage,
            meetingName: meeting.data.meetingName,
            note: meeting.data.note,
            sportId: meeting.data.sportId,
            startDate: meeting.data.startDate,
            totalMember: meeting.data.totalMember,
            sportId: 2 // Nếu sportId là cần thiết ở đây
        };

        const response = await api.createMeeting(payload);
        Alert.alert('Success', 'Meeting created successfully!');
        console.log(response);
    } catch (error: any) {
        console.error('API Error: ', error.response?.data);
        Alert.alert('Error', 'Failed to create the meeting.');
    }
};

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.backButton} onPress={() => { }}>
          <Ionicons name="arrow-back-circle-outline" color={"#ffff"} size={30} />
        </TouchableOpacity>
        <Text style={styles.headerText}>CREATE A MEET</Text>
      </View>

      <ScrollView contentContainerStyle={styles.scrollContainer}>
        <View style={styles.sportOptions}>
          <SportChoose setSport={(sportId) => {
            setSport(sportId);
            setMeeting((prevMeeting) => ({ ...prevMeeting, sportId }));
          }} />
        </View>

        <Text style={styles.sectionTitle}>MEET</Text>
        <View style={styles.form}>
          <View style={styles.formItem}>
            <View style={styles.formLabelContainer}>
              <Ionicons name="calendar-outline" color={"#ff951d"} size={20} />
              <Text style={styles.formLabel}>Select date</Text>
            </View>
            <TouchableOpacity onPress={() => showDateTimePicker(true)}>
              <Text>{meeting.data.startDate ? `Start Date: ${meeting.data.startDate}` : 'Select Start Date'}</Text>
            </TouchableOpacity>




            <TouchableOpacity onPress={() => showDateTimePicker(false)}>
              <Text>{meeting.data.endDate ? `End Date: ${meeting.data.endDate}` : 'Select End Date'}</Text>
            </TouchableOpacity>
            <DateTimePickerModal
              isVisible={isDatePickerVisible}
              mode="datetime"
              onConfirm={handleConfirmDate}
              onCancel={hideDateTimePicker}
            />
          </View>

          <View style={styles.formItemMeeting}>
            <View style={styles.formLabelContainer}>
              <Ionicons name="location-outline" color={"#ff951d"} size={20} />
              <Text style={styles.formLabel}>Set location</Text>
            </View>
            <TextInput
              style={styles.formInputMeeting}
              value={meeting.data.address}
              onChangeText={(value) => handleInputChange('address', value)}
              placeholder="Enter location"
            />
          </View>

          <View style={styles.formItem}>
            <Text style={styles.formLabel}>Set time to cancel</Text>
            <TextInput
              style={styles.formInput}
              value={meeting.data.cancelBefore.toString()}
              onChangeText={(value) => handleInputChange('cancelBefore', value)}
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
              <TouchableOpacity onPress={incrementMember} style={styles.counterButton}>
                <Text style={styles.counterButtonText}>-</Text>
              </TouchableOpacity>
              <Text style={styles.counterText}>{meeting.data.totalMember}</Text>
              <TouchableOpacity onPress={decrementMember} style={styles.counterButton}>
                <Text style={styles.counterButtonText}>+</Text>
              </TouchableOpacity>
            </View>
          </View>

          <View style={styles.formItemMeeting}>
            <Text style={styles.formLabelMeeting}>Meet Name</Text>
            <TextInput
              style={styles.formInputMeeting}
              value={meeting.data.meetingName}
              onChangeText={(value) => handleInputChange('meetingName', value)}
              placeholder="Enter meet name"
            />
            <TextInput
              style={styles.formInputMeeting}
              value={meeting.data.note}
              onChangeText={(value) => handleInputChange('note', value)}
              multiline={true}
              placeholder="Add notes"
            />
          </View>
        </View>
        <View style={styles.buttonContainer}>
          <Button title="Create Meet" onPress={handleCreateMeeting} color="#f9ca24" />
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
