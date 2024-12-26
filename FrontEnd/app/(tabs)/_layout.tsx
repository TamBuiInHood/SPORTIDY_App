import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { useColorScheme } from '@/hooks/useColorScheme';
import { TabBarIcon } from '@/components/navigation/TabBarIcon';
import account from './account';
import YourMeeting from './club';
import CreateMeetScreen from './create';
import BookingScreen from './booking';
import HomeScreen from '.';
import { TabParamList } from '@/types/types';

const Tab = createBottomTabNavigator<TabParamList>();

const TabLayout: React.FC = () => {
  const colorScheme = useColorScheme();

  return (
    <Tab.Navigator
      initialRouteName="index"
      screenOptions={({ route }) => ({
        tabBarActiveTintColor: '#ff591d',
        tabBarInactiveTintColor: '#ddd',
        tabBarShowLabel: true,
        tabBarStyle: {
          backgroundColor: '#ffffff',
          borderTopLeftRadius: 20,
          borderTopRightRadius: 20,
          paddingBottom: 5,
          height: 80,
          position: 'absolute',
          left: 0,
          right: 0,
          bottom: 0,
          elevation: 0,
          borderTopWidth: 1,
          borderTopColor: "#ddd",
        },
        tabBarItemStyle: {
          paddingBottom: 10,
        },
        headerShown: false,
      })}
    >
      <Tab.Screen
        name="index"
        component={HomeScreen}
        options={{
          title: 'Home',
          tabBarIcon: ({ color, focused, size }) => (
            <TabBarIcon name={focused ? 'home' : 'home-outline'} color={color} size={size} />
          ),
        }}
      />
      <Tab.Screen
        name="booking"
        component={BookingScreen}
        options={{
          title: 'Booking',
          tabBarIcon: ({ color, focused, size }) => (
            <TabBarIcon name={focused ? 'calendar' : 'calendar-outline'} color={color} size={size} />
          ),
        }}
      />
      <Tab.Screen
        name="create"
        component={CreateMeetScreen}
        options={{
          title: '',
          tabBarIcon: ({ color, focused, size }) => (
            <TabBarIcon name={focused ? 'add-circle' : 'add-circle-outline'} color={color} size={size} />
          ),
        }}
      />
      <Tab.Screen
        name="club"
        component={YourMeeting}
        options={{
          title: 'Club',
          tabBarIcon: ({ color, focused, size }) => (
            <TabBarIcon name={focused ? 'people' : 'people-outline'} color={color} size={size} />
          ),
        }}
      />
      <Tab.Screen
        name="account"
        component={account}
        options={{
          title: 'Account',
          tabBarIcon: ({ color, focused, size }) => (
            <TabBarIcon name={focused ? 'person' : 'person-outline'} color={color} size={size} />
          ),
        }}
      />
    </Tab.Navigator>
  );
};

export default TabLayout;