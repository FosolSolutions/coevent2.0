import styled from 'styled-components';

export const ActivityOpening = styled.div`
  display: flex;
  flex-direction: column;
  border: solid 1px ${(props) => props.theme.css.selectedColor};
  border-radius: 0.25em;

  .applicant {
    margin-top: auto;
    margin-bottom: auto;
    display: flex;
    flex-direction: column;
    align-items: center;
    align-self: center;
    gap: 0.5em;

    > * {
      display: flex;
      align-items: center;
    }

    .btn.edit {
      color: ${(props) => props.theme.css.primaryColor};

      &:hover {
        filter: brightness(200%);
        cursor: pointer;
      }
    }
  }
`;
