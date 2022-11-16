import React from 'react';
import { FaMinusCircle, FaPlusCircle } from 'react-icons/fa';

import * as styled from './styled';

export interface IApplyButtonProps {
  canApply?: boolean;
  onClick?: () => void;
}

export const ApplyButton: React.FC<IApplyButtonProps> = ({ canApply = true, onClick }) => {
  return (
    <styled.ApplyButton
      className={`btn ${canApply ? 'on' : 'off'}`}
      onClick={() => {
        onClick?.();
      }}
    >
      {canApply ? (
        <FaPlusCircle className="off" title="Apply" size={30} />
      ) : (
        <FaMinusCircle className="on" title="Withdraw" size={30} />
      )}
    </styled.ApplyButton>
  );
};
