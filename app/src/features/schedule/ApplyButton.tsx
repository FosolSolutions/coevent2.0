import React from 'react';
import { FaMinusCircle, FaRegCircle } from 'react-icons/fa';

import * as styled from './styled';

export interface IApplyButtonProps {
  active?: boolean;
  onClick?: () => void;
}

export const ApplyButton: React.FC<IApplyButtonProps> = ({ active = false, onClick }) => {
  return (
    <styled.ApplyButton
      className={`btn ${active ? 'on' : 'off'}`}
      onClick={() => {
        onClick?.();
      }}
    >
      {active ? (
        <FaMinusCircle className="off" title="Withdraw" size={30} />
      ) : (
        <FaRegCircle className="on" title="Apply" size={30} />
      )}
    </styled.ApplyButton>
  );
};
