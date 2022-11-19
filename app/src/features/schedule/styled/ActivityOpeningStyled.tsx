import styled from 'styled-components';

export const ActivityOpening = styled.div`
  display: flex;
  flex-direction: row;
  border: solid 1px ${(props) => props.theme.css.selectedColor};
  border-radius: 0.25em;
  align-items: stretch;
  align-content: stretch;

  & > * {
    width: 100%;
    height: 100%;
    gap: 0.25em;
  }

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

  .message {
    margin-left: auto;
    margin-right: auto;
    display: flex;
    flex-direction: column;
    align-items: flex-end;

    > * {
      display: flex;
      align-items: center;
      flex: 1 1 auto;
    }
  }
`;
