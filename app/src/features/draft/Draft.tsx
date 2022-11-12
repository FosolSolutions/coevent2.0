import ConstructionImg from './construction.jpg';
import * as styled from './DraftStyled';

export const Draft = () => {
  return (
    <styled.Draft>
      <div>
        <h1>Under Construction</h1>
      </div>
      <img src={ConstructionImg} alt="Under Construction" />
    </styled.Draft>
  );
};
