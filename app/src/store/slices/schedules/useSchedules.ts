import { IActivityOpeningModel, IScheduleModel } from 'hooks';
import React from 'react';
import { useAppDispatch, useAppSelector } from 'store';

import { storeSchedule, updateOpening } from '.';
import { IScheduleState } from './IScheduleState';

export interface IScheduleStore {
  storeSchedule: (schedule: IScheduleModel) => void;
  updateOpening: (opening: IActivityOpeningModel) => void;
}

export const useSchedules = (): [IScheduleState, IScheduleStore] => {
  const dispatch = useAppDispatch();
  const state = useAppSelector((store) => store.schedules);

  const controller = React.useMemo(
    () => ({
      storeSchedule: (schedule: IScheduleModel) => {
        dispatch(storeSchedule(schedule));
      },
      updateOpening: (opening: IActivityOpeningModel) => {
        dispatch(updateOpening(opening));
      },
    }),
    [dispatch],
  );

  return [state, controller];
};
