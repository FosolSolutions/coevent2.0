import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IActivityOpeningModel, IScheduleModel } from 'hooks';

import { IScheduleState } from './IScheduleState';

const initialState: IScheduleState = {};

export const schedulesSlice = createSlice({
  name: 'schedules',
  initialState: initialState,
  reducers: {
    storeSchedule(state: IScheduleState, action: PayloadAction<IScheduleModel>) {
      state.schedule = action.payload;
    },
    updateOpening(state: IScheduleState, action: PayloadAction<IActivityOpeningModel>) {
      const event = state.schedule?.events.find((e) =>
        e.activities.find((a) => a.id === action.payload.activityId),
      );
      const activity = event?.activities.find((a) => a.id === action.payload.activityId);
      const opening = activity?.openings.find((o) => o.id === action.payload.id);

      if (activity && opening) {
        // Replace the opening with the one provided.
        activity.openings = [
          ...activity.openings.filter((o) => o.id !== opening.id),
          action.payload,
        ];
      }
    },
  },
});

export const { storeSchedule, updateOpening } = schedulesSlice.actions;
