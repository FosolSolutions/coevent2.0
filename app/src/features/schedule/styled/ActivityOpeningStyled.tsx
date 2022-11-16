import styled from 'styled-components';

export const ActivityOpening = styled.div`
  display: flex;
  flex-direction: column;

  .applicant {
    display: flex;
    flex-direction: row;
    gap: 0.5em;

    > * {
      display: flex;
      align-items: center;
    }
  }
`;
