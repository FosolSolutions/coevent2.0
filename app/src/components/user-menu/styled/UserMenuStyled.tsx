import styled from 'styled-components';

export const UserMenu = styled.div`
  display: flex;
  flex-direction: row;
  align-content: center;
  align-items: center;
  height: 100%;

  > div:first-child {
    margin-right: 10px;
    font-size: 0.75em;
    font-weight: normal;
  }
`;

export default UserMenu;
