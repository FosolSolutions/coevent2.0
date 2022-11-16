import styled from 'styled-components';

export const ApplyButton = styled.div`
  cursor: pointer;

  svg.on {
    color: ${(props) => props.theme.css.dangerColor};
  }

  svg.off {
    color: ${(props) => props.theme.css.primaryColor};
  }
`;
